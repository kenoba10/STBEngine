using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using STBEngine.Core;
using STBEngine.Rendering;

namespace STBEngine.Rendering
{

	public struct GUIObject
	{

		private int vao;

		private int pbo;
		private int tbo;

		private bool useTexture;
		private Color4 color;
		private Texture texture;

		public GUIObject(int vao, int pbo, int tbo, bool useTexture, Color4 color, Texture texture)
		{

			this.vao = vao;

			this.pbo = pbo;
			this.tbo = tbo;

			this.useTexture = useTexture;
			this.color = color;
			this.texture = texture;

		}

		public int VAO
		{

			get
			{

				return vao;

			}

		}

		public int PBO
		{

			get
			{

				return pbo;

			}

		}

		public int TBO
		{

			get
			{

				return tbo;

			}

		}

		public bool UseTexture
		{

			get
			{

				return useTexture;

			}

		}

		public Color4 Color
		{

			get
			{

				return color;

			}

		}

		public Texture Texture
		{

			get
			{

				return texture;

			}

		}

	}

	public abstract class GUI
	{

		private List<GUIObject> backgroundObjects;
		private List<GUIObject> foregroundObjects;

		private Shader shader;

		public GUI()
		{

			backgroundObjects = new List<GUIObject>();
			foregroundObjects = new List<GUIObject>();

			shader = new Shader();

		}

		public void Initialize()
		{

			shader.AddVertexShader(Shader.GUI_VERTEX_SHADER);
			shader.AddGeometryShader(Shader.GUI_GEOMETRY_SHADER);
			shader.AddFragmentShader(Shader.GUI_FRAGMENT_SHADER);

			shader.Compile();

			shader.Bind();
			
			shader.SetUniform("projection", Matrix4.CreateOrthographicOffCenter(0f, 720f, 480f, 0f, -1f, 1f));

			shader.UnBind();

			Draw();

		}

		public void DrawBackground()
		{

			shader.Bind();

			shader.SetUniform("depth", 0.0001f);

			foreach(GUIObject guiObject in backgroundObjects)
			{

				shader.SetUniform("useTexture", guiObject.UseTexture ? 1 : 0);
				shader.SetUniform("baseColor", new Vector4(guiObject.Color.R, guiObject.Color.G, guiObject.Color.B, guiObject.Color.A));

				if(guiObject.UseTexture)
				{

					guiObject.Texture.Bind();

				}

				GL.BindVertexArray(guiObject.VAO);

				GL.EnableVertexAttribArray(0);
				GL.EnableVertexAttribArray(1);

				GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

				GL.DisableVertexAttribArray(1);
				GL.DisableVertexAttribArray(0);

				GL.BindVertexArray(0);

				if(guiObject.UseTexture)
				{

					guiObject.Texture.UnBind();

				}

			}

			shader.UnBind();

		}

		public void DrawForeground(float depthIndex)
		{

			shader.Bind();

			foreach(GUIObject guiObject in foregroundObjects)
			{

				shader.SetUniform("depth", depthIndex);

				shader.SetUniform("useTexture", guiObject.UseTexture ? 1 : 0);
				shader.SetUniform("baseColor", new Vector4(guiObject.Color.R, guiObject.Color.G, guiObject.Color.B, guiObject.Color.A));

				if(guiObject.UseTexture)
				{

					guiObject.Texture.Bind();

				}

				GL.BindVertexArray(guiObject.VAO);

				GL.EnableVertexAttribArray(0);
				GL.EnableVertexAttribArray(1);

				GL.DrawArrays(PrimitiveType.Triangles, 0, 6);

				GL.DisableVertexAttribArray(1);
				GL.DisableVertexAttribArray(0);

				GL.BindVertexArray(0);

				if(guiObject.UseTexture)
				{

					guiObject.Texture.UnBind();

				}

				depthIndex += 0.0002f;

			}

			shader.UnBind();

		}

		public void Terminate()
		{

			foreach(GUIObject guiObject in foregroundObjects)
			{

				GL.DeleteBuffer(guiObject.TBO);
				GL.DeleteBuffer(guiObject.PBO);

				GL.DeleteVertexArray(guiObject.VAO);

			}

			foreach(GUIObject guiObject in backgroundObjects)
			{

				GL.DeleteBuffer(guiObject.TBO);
				GL.DeleteBuffer(guiObject.PBO);

				GL.DeleteVertexArray(guiObject.VAO);

			}

		}

		public abstract void Draw();
		public abstract void Update();

		protected int DrawRectangle(Vector2 position, Vector2 size, Color4 color, bool foreground)
		{

			Vector2[] positions = new Vector2[] { position, position + new Vector2(size.X, 0f), position + size, position, position + size, position + new Vector2(0f, size.Y) };
			Vector2[] textureCoordinates = new Vector2[] { new Vector2(0f, 1f), new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(0f, 1f), new Vector2(1f, 0f), new Vector2(1f, 1f) };

			int vao = GL.GenVertexArray();

			GL.BindVertexArray(vao);

			int pbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, pbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector2.SizeInBytes * 6), positions, BufferUsageHint.StreamDraw);

			GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			int tbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, tbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector2.SizeInBytes * 6), textureCoordinates, BufferUsageHint.StreamDraw);

			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			GL.BindVertexArray(0);

			if(foreground)
			{
			
				foregroundObjects.Add(new GUIObject(vao, pbo, tbo, false, color, null));

				return foregroundObjects.Count - 1;

			}
			else
			{

				backgroundObjects.Add(new GUIObject(vao, pbo, tbo, false, color, null));

				return backgroundObjects.Count - 1;

			}

		}

		protected int DrawRectangle(Vector2 position, Vector2 size, Texture texture, bool foreground)
		{

			Vector2[] positions = new Vector2[] { position, position + new Vector2(size.X, 0f), position + size, position, position + size, position + new Vector2(0f, size.Y) };
			Vector2[] textureCoordinates = new Vector2[] { new Vector2(0f, 0f), new Vector2(1f, 0f), new Vector2(1f, 1f), new Vector2(0f, 0f), new Vector2(1f, 1f), new Vector2(0f, 1f) };

			int vao = GL.GenVertexArray();

			GL.BindVertexArray(vao);

			int pbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, pbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector2.SizeInBytes * 6), positions, BufferUsageHint.StreamDraw);

			GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			int tbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, tbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector2.SizeInBytes * 6), textureCoordinates, BufferUsageHint.StreamDraw);

			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			GL.BindVertexArray(0);

			if(foreground)
			{

				foregroundObjects.Add(new GUIObject(vao, pbo, tbo, true, Color4.White, texture));

				return foregroundObjects.Count - 1;

			}
			else
			{

				backgroundObjects.Add(new GUIObject(vao, pbo, tbo, true, Color4.White, texture));

				return backgroundObjects.Count - 1;

			}

		}

		protected int DrawString(Vector2 position, string text, Font font, Color color)
		{

			Bitmap bitmap = new Bitmap(720, 480, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			using(Graphics gfx = Graphics.FromImage(bitmap))
			{

				gfx.Clear(Color.Transparent);

				gfx.DrawString(text, font, new SolidBrush(color), new PointF(position.X, position.Y));

			}

			IntPtr data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb).Scan0;

			Texture texture = new Texture();
			texture.LoadTexture(data, bitmap.Width, bitmap.Height);

			return DrawRectangle(new Vector2(0, 0), new Vector2(720, 480), texture, true);

		}

		protected void AddGUIObject(GUIObject guiObject, bool foreground)
		{

			if(foreground)
			{

				foregroundObjects.Add(guiObject);

			}
			else
			{

				backgroundObjects.Add(guiObject);

			}

		}

		protected void RemoveGUIObject(GUIObject guiObject, bool foreground)
		{

			if(foreground)
			{

				foregroundObjects.Remove(guiObject);

			}
			else
			{

				backgroundObjects.Remove(guiObject);

			}

		}

		protected List<GUIObject> BackgroundObjects
		{

			get
			{

				return backgroundObjects;

			}

		}

		protected List<GUIObject> ForegroundObjects
		{

			get
			{

				return foregroundObjects;

			}

		}

	}

}