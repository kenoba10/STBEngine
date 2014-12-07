using System;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using STBEngine.Core;
using STBEngine.Core.Components;

namespace STBEngine.Rendering
{

	public class RenderingEngine
	{

		private Camera camera;

		public RenderingEngine()
		{

			camera = new Camera();

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

		public void Render(Entity entity)
		{

			entity.Shader.Bind();

			entity.Mesh.Draw();

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
			GL.Enable(EnableCap.FramebufferSrgb);
			
			GL.FrontFace(FrontFaceDirection.Cw);
			GL.CullFace(CullFaceMode.Back);

		}

		public string GetOpenGLVersion()
		{

			return GL.GetInteger(GetPName.MajorVersion) + "." + GL.GetInteger(GetPName.MinorVersion);

		}

		public void SetClearColor(Color4 color)
		{

			GL.ClearColor(color);

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