using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using STBEngine.Core;
using STBEngine.Core.Components;

namespace STBEngine.Rendering
{

	public class RenderingEngine
	{

		private Camera camera;

		private GUI openGUI;

		private List<GUI> guis;

		private List<DirectionalLight> directionalLights;
		private List<PointLight> pointLights;
		private List<SpotLight> spotLights;

		public RenderingEngine()
		{

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

			float depthIndex = 0.0003f;

			if(openGUI != null)
			{

				openGUI.DrawBackground();
				openGUI.DrawForeground(0.0002f);

			}

			foreach(GUI gui in guis)
			{

				gui.DrawBackground();

			}

			foreach(GUI gui in guis)
			{

				gui.DrawForeground(depthIndex);

				depthIndex += 0.0064f;

			}

		}

		public void Terminate()
		{

		}

		public void UpdateUniforms(Entity entity)
		{

			Shader shader = entity.Shader;

			shader.Bind();

			shader.SetUniform("projection", CoreEngine.Instance.RenderingEngine.Camera.Projection);
			shader.SetUniform("transformation", entity.Transformation.GetTransformation());

			shader.SetUniform("eyePosition", CoreEngine.Instance.RenderingEngine.Camera.Parent.Transformation.Position);

			shader.SetUniform("useTexture", entity.Material.Texture.Initialized ? 1 : 0);
			shader.SetUniform("baseColor", new Vector4(entity.Material.Color.R, entity.Material.Color.G, entity.Material.Color.B, entity.Material.Color.A));

			shader.SetUniform("ambientLight", entity.Material.AmbientLight);

			for(uint i = 0; i < directionalLights.Count; i++)
			{

				SetUniformDirectionalLight(i, directionalLights[(int) i], shader);

			}

			for(uint i = 0; i < pointLights.Count; i++)
			{

				SetUniformPointLight(i, pointLights[(int) i], shader);

			}

			for(uint i = 0; i < spotLights.Count; i++)
			{

				SetUniformSpotLight(i, spotLights[(int) i], shader);

			}

			shader.SetUniform("specularIntensity", entity.Material.SpecularIntensity);
			shader.SetUniform("specularExponent", entity.Material.SpecularExponent);

			shader.UnBind();

		}

		public void Render(Entity entity)
		{

			entity.Shader.Bind();

			entity.Material.Texture.Bind();

			entity.Mesh.Draw();

			entity.Material.Texture.UnBind();

			entity.Shader.UnBind();

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

		private void SetUniformDirectionalLight(uint index, DirectionalLight light, Shader shader)
		{

			shader.SetUniform("directionalLights[" + index + "].base.color", new Vector4(light.Base.Color.R, light.Base.Color.G, light.Base.Color.B, light.Base.Color.A));
			shader.SetUniform("directionalLights[" + index + "].base.intensity", light.Base.Intensity);
			shader.SetUniform("directionalLights[" + index + "].direction", light.Direction);

		}

		private void SetUniformPointLight(uint index, PointLight light, Shader shader)
		{

			shader.SetUniform("pointLights[" + index + "].base.color", new Vector4(light.Base.Color.R, light.Base.Color.G, light.Base.Color.B, light.Base.Color.A));
			shader.SetUniform("pointLights[" + index + "].base.intensity", light.Base.Intensity);
			shader.SetUniform("pointLights[" + index + "].attenuation.constant", light.Attenuation.Constant);
			shader.SetUniform("pointLights[" + index + "].attenuation.linear", light.Attenuation.Linear);
			shader.SetUniform("pointLights[" + index + "].attenuation.exponent", light.Attenuation.Exponent);
			shader.SetUniform("pointLights[" + index + "].position", light.Position);
			shader.SetUniform("pointLights[" + index + "].range", light.Range);

		}

		private void SetUniformSpotLight(uint index, SpotLight light, Shader shader)
		{

			shader.SetUniform("spotLights[" + index + "].base.base.color", new Vector4(light.Base.Base.Color.R, light.Base.Base.Color.G, light.Base.Base.Color.B, light.Base.Base.Color.A));
			shader.SetUniform("spotLights[" + index + "].base.base.intensity", light.Base.Base.Intensity);
			shader.SetUniform("spotLights[" + index + "].base.attenuation.constant", light.Base.Attenuation.Constant);
			shader.SetUniform("spotLights[" + index + "].base.attenuation.linear", light.Base.Attenuation.Linear);
			shader.SetUniform("spotLights[" + index + "].base.attenuation.exponent", light.Base.Attenuation.Exponent);
			shader.SetUniform("spotLights[" + index + "].base.position", light.Base.Position);
			shader.SetUniform("spotLights[" + index + "].base.range", light.Base.Range);
			shader.SetUniform("spotLights[" + index + "].direction", light.Direction);
			shader.SetUniform("spotLights[" + index + "].cutoff", light.CutOff);

		}

		public void OpenGUI(GUI gui)
		{

			CoreEngine.Instance.EventHandler.Execute("openGUI");

			openGUI = gui;

			openGUI.Initialize();

		}

		public void CloseGUI()
		{

			openGUI.Terminate();

			openGUI = null;

			CoreEngine.Instance.EventHandler.Execute("closeGUI");

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