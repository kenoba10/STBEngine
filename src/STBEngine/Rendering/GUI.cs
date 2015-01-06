using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using STBEngine.Core;
using STBEngine.Rendering;
using STBEngine.Rendering.Shaders;

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

		private List<GUIObject> guiObjects;

		public GUI()
		{

			guiObjects = new List<GUIObject>();

		}

		public void Initialize()
		{

			Draw();

		}

		public void Render()
		{

			GUIShader.Instance.Bind();

			GL.DepthMask(false);
			GL.DepthFunc(DepthFunction.Always);

			foreach(GUIObject guiObject in guiObjects)
			{

				GUIShader.Instance.UpdateUniforms(guiObject);

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

			GL.DepthFunc(DepthFunction.Less);
			GL.DepthMask(true);

			GUIShader.Instance.UnBind();

		}

		public void Terminate()
		{

			foreach(GUIObject guiObject in guiObjects)
			{

				GL.DeleteBuffer(guiObject.TBO);
				GL.DeleteBuffer(guiObject.PBO);

				GL.DeleteVertexArray(guiObject.VAO);

			}

			GUIShader.Instance.Delete();

		}

		public abstract void Draw();
		public abstract void Update();

		protected int DrawRectangle(Vector2 position, Vector2 size, Color4 color)
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

			guiObjects.Add(new GUIObject(vao, pbo, tbo, false, color, null));

			return guiObjects.Count - 1;

		}

		protected int DrawRectangle(Vector2 position, Vector2 size, Texture texture)
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

			guiObjects.Add(new GUIObject(vao, pbo, tbo, true, Color4.White, texture));

			return guiObjects.Count - 1;

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

			return DrawRectangle(new Vector2(0, 0), new Vector2(720, 480), texture);

		}

		protected void AddGUIObject(GUIObject guiObject)
		{

			guiObjects.Add(guiObject);

		}

		protected void RemoveGUIObject(GUIObject guiObject)
		{

			guiObjects.Remove(guiObject);

		}

		protected List<GUIObject> GUIObjects
		{

			get
			{

				return guiObjects;

			}

		}

	}

}