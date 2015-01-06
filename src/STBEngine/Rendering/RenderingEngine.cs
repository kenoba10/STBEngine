using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

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

			if(openGUI != null)
			{

				openGUI.Update();

				if(Input.GetKeyDown(Key.Escape))
				{

					CloseGUI();

				}

			}

			foreach(GUI gui in guis)
			{

				gui.Update();

			}

		}

		public void Render()
		{

			if(openGUI != null)
			{

				openGUI.Render();

			}

			foreach(GUI gui in guis)
			{

				gui.Render();

			}

		}

		public void Terminate()
		{

		}

		public void Render(Entity entity)
		{

			BasicShader.Instance.Bind();

			BasicShader.Instance.UpdateUniforms(engine, entity);

			entity.Material.Texture.Bind();

			entity.Mesh.Draw();

			entity.Material.Texture.UnBind();

			BasicShader.Instance.UnBind();

			GL.DepthMask(false);
			GL.DepthFunc(DepthFunction.Equal);
			GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);

			foreach(DirectionalLight light in directionalLights)
			{

				DirectionalLightShader.Instance.Bind();

				DirectionalLightShader.Instance.UpdateUniforms(engine, entity, light);

				entity.Mesh.Draw();

				DirectionalLightShader.Instance.UnBind();

			}

			foreach(PointLight light in pointLights)
			{

				PointLightShader.Instance.Bind();

				PointLightShader.Instance.UpdateUniforms(engine, entity, light);

				entity.Mesh.Draw();

				PointLightShader.Instance.UnBind();

			}

			foreach(SpotLight light in spotLights)
			{

				SpotLightShader.Instance.Bind();

				SpotLightShader.Instance.UpdateUniforms(engine, entity, light);

				entity.Mesh.Draw();

				SpotLightShader.Instance.UnBind();

			}

			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.DepthFunc(DepthFunction.Less);
			GL.DepthMask(true);

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