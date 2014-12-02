using System;

using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace STBEngine.Rendering
{

	public class RenderingEngine
	{

		public void Initialize()
		{

			InitializeOpenGL();
			SetClearColor(Color4.Black);

		}

		public void Update()
		{

		}

		public void Terminate()
		{

		}

		public void ClearScreen()
		{

			GL.Clear(ClearBufferMask.ColorBufferBit);

		}

		public void ResizeScreen(uint width, uint height)
		{

			GL.Viewport(0, 0, (int) width, (int) height);

		}

		public void InitializeOpenGL()
		{

			GL.Enable(EnableCap.CullFace);
			GL.Enable(EnableCap.FramebufferSrgb);

			GL.FrontFace(FrontFaceDirection.Cw);
			GL.CullFace(CullFaceMode.Back);

		}

		public void SetClearColor(Color4 color)
		{

			GL.ClearColor(color);

		}

	}

}