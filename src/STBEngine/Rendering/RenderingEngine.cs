using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using STBEngine.Core;
using STBEngine.Core.Components;

namespace STBEngine.Rendering
{

	public class RenderingEngine
	{

		private Camera camera;

		private List<DirectionalLight> directionalLights;
		private List<PointLight> pointLights;

		public RenderingEngine()
		{

			camera = new Camera();

			directionalLights = new List<DirectionalLight>();
			pointLights = new List<PointLight>();

		}

		public void Initialize()
		{

			InitializeOpenGL();
			SetClearColor(Color4.Black);

			Console.WriteLine("OpenGL Version: " + GetOpenGLVersion());

		}

		public void Update()
		{

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
			
			GL.FrontFace(FrontFaceDirection.Cw);
			GL.CullFace(CullFaceMode.Back);

			GL.DepthFunc(DepthFunction.Less);

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

	}

}