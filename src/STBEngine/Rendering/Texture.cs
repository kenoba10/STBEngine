using System;
using System.IO;

using OpenTK.Graphics.OpenGL;

using STBEngine.Utilities;

namespace STBEngine.Rendering
{

	public class Texture
	{

		private int texture;

		public Texture()
		{

			texture = 0;

		}

		public void LoadTexture(Stream stream)
		{

			int width;
			int height;

			IntPtr data = IOUtils.LoadTexture(stream, out width, out height);

			texture = GL.GenTexture();

			GL.BindTexture(TextureTarget.Texture2D, texture);

			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float) TextureMinFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float) TextureMagFilter.Linear);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float) TextureWrapMode.Repeat);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float) TextureWrapMode.Repeat);

			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data);

			GL.BindTexture(TextureTarget.Texture2D, 0);

		}

		public void Bind()
		{

			GL.BindTexture(TextureTarget.Texture2D, texture);

		}

		public void UnBind()
		{

			GL.BindTexture(TextureTarget.Texture2D, 0);

		}

		public void UnloadTexture()
		{

			GL.DeleteTexture(texture);

		}

	}

}