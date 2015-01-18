using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

using STBEngine.Core;
using STBEngine.Core.Components;
using STBEngine.Rendering.Shaders;

namespace STBEngine.Rendering
{

	public class RenderingEngine
	{

		private CoreEngine engine;

		private Camera camera;

		private GUI openGUI;

		private List<GUI> guis;

		private List<DirectionalLight> directionalLights;
		private List<PointLight> pointLights;
		private List<SpotLight> spotLights;

		public RenderingEngine(CoreEngine engine)
		{

			this.engine = engine;

			camera = new Camera();

			guis = new List<GUI>();

			directionalLights = new List<DirectionalLight>();
			pointLights = new List<PointLight>();
			spotLights = new List<SpotLight>();

		}

		public void Initialize()
		{

			InitializeOpenGL();
			SetClearColor(Color4.Black);

			Console.WriteLine("OpenGL Version: " + GetOpenGLVersion());

		}

		public void Update()
		{

			foreach(GUI gui in guis)
			{

				gui.Update();

			}

			if(openGUI != null)
			{

				openGUI.Update();

				if(Input.GetKeyDown(Key.Escape))
				{

					CloseGUI();

				}

			}

		}

		public void Render()
		{

			foreach(GUI gui in guis)
			{

				gui.Render();

			}

			if(openGUI != null)
			{

				openGUI.Render();

			}

		}

		public void Terminate()
		{

			GUIShader.Instance.Delete();

			SpotLightShader.Instance.Delete();
			PointLightShader.Instance.Delete();
			DirectionalLightShader.Instance.Delete();

			BasicShader.Instance.Delete();

		}

		public void Render(Entity entity)
		{
			
			entity.Material.DisplacementMap.Bind(TextureUnit.Texture0);

			BasicShader.Instance.Bind();

			BasicShader.Instance.UpdateUniforms(engine, entity);
			
			entity.Material.Texture.Bind(TextureUnit.Texture1);

			entity.Render();

			entity.Material.Texture.UnBind();

			BasicShader.Instance.UnBind();

			GL.DepthMask(false);
			GL.DepthFunc(DepthFunction.Equal);
			GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
			
			entity.Material.NormalMap.Bind(TextureUnit.Texture1);

			DirectionalLightShader.Instance.Bind();

			foreach(DirectionalLight light in directionalLights)
			{

				DirectionalLightShader.Instance.UpdateUniforms(engine, entity, light);

				entity.Render();

			}

			DirectionalLightShader.Instance.UnBind();

			PointLightShader.Instance.Bind();

			foreach(PointLight light in pointLights)
			{

				PointLightShader.Instance.UpdateUniforms(engine, entity, light);

				entity.Render();

			}

			PointLightShader.Instance.UnBind();

			SpotLightShader.Instance.Bind();

			foreach(SpotLight light in spotLights)
			{

				SpotLightShader.Instance.UpdateUniforms(engine, entity, light);

				entity.Render();

			}

			SpotLightShader.Instance.UnBind();

			entity.Material.NormalMap.UnBind();

			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.DepthFunc(DepthFunction.Less);
			GL.DepthMask(true);

			entity.Material.DisplacementMap.UnBind();

		}

		public void ClearScreen()
		{

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

		}

		public void ResizeScreen(uint width, uint height)
		{

			GL.Viewport(0, 0, (int) width, (int) height);

			camera.Width = width;
			camera.Height = height;

		}

		public void InitializeOpenGL()
		{

			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.DepthTest);
			GL.Enable(EnableCap.DepthClamp);
			GL.Enable(EnableCap.Blend);
			
			GL.FrontFace(FrontFaceDirection.Cw);
			GL.CullFace(CullFaceMode.Back);

			GL.DepthFunc(DepthFunction.Less);

			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

		}

		public string GetOpenGLVersion()
		{

			return GL.GetInteger(GetPName.MajorVersion) + "." + GL.GetInteger(GetPName.MinorVersion);

		}

		public void SetClearColor(Color4 color)
		{

			GL.ClearColor(color);

		}

		public void OpenGUI(GUI gui)
		{

			CloseGUI();

			engine.EventHandler.Execute("openGUI");

			openGUI = gui;

			openGUI.Initialize();

		}

		public void CloseGUI()
		{

			if(openGUI != null)
			{

				openGUI.Terminate();

				openGUI = null;

				engine.EventHandler.Execute("closeGUI");

			}

		}

		public void AddGUI(GUI gui)
		{

			gui.Initialize();

			guis.Add(gui);

		}

		public void RemoveGUI(GUI gui)
		{

			guis.Remove(gui);

			gui.Terminate();

		}

		public void AddDirectionalLight(DirectionalLight light)
		{

			directionalLights.Add(light);

		}

		public void RemoveDirectionalLight(DirectionalLight light)
		{

			directionalLights.Remove(light);

		}

		public void AddPointLight(PointLight light)
		{

			pointLights.Add(light);

		}

		public void RemovePointLight(PointLight light)
		{

			pointLights.Remove(light);

		}

		public void AddSpotLight(SpotLight light)
		{

			spotLights.Add(light);

		}

		public void RemoveSpotLight(SpotLight light)
		{

			spotLights.Remove(light);

		}

		public Camera Camera
		{

			get
			{

				return camera;

			}
			set
			{

				this.camera = value;

			}

		}

		public List<GUI> GUIs
		{

			get
			{

				return guis;

			}

		}

	}

}