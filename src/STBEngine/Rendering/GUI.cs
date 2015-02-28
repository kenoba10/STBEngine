using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

using STBEngine.Rendering.Shaders;

namespace STBEngine.Rendering
{

	public struct GUIObject
	{

		private int vao;

		private int pbo;
		private int ubo;

		private bool useTexture;
		private Color4 color;
		private Texture texture;

		public GUIObject(int vao, int pbo, int ubo, bool useTexture, Color4 color, Texture texture)
		{

			this.vao = vao;

			this.pbo = pbo;
			this.ubo = ubo;

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

		public int UBO
		{

			get
			{

				return ubo;

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
			set
			{

				this.color = value;

			}

		}

		public Texture Texture
		{

			get
			{

				return texture;

			}
			set
			{

				this.texture = value;

			}

		}

	}

	public abstract class GUI
	{

		public static readonly float WIDTH = 1920f;
		public static readonly float HEIGHT = 1080f;

		private List<GUIObject> guiObjects;

		public GUI()
		{

			guiObjects = new List<GUIObject>();

		}

		public virtual void Initialize()
		{

			Draw();

		}

		public void Render()
		{

			GUIShader.Instance.Bind();

			GL.DepthMask(false);
			GL.DepthFunc(DepthFunction.Always);

			RenderC();

			foreach(GUIObject guiObject in guiObjects)
			{

				GUIShader.Instance.UpdateUniforms(guiObject);

				if(guiObject.UseTexture)
				{

					guiObject.Texture.Bind(TextureUnit.Texture0);

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

		public virtual void Terminate()
		{

			foreach(GUIObject guiObject in guiObjects)
			{

				GL.DeleteBuffer(guiObject.UBO);
				GL.DeleteBuffer(guiObject.PBO);

				GL.DeleteVertexArray(guiObject.VAO);

			}

			guiObjects.Clear();

		}

		public abstract void Draw();
		public abstract void Update();
		public abstract void RenderC();

		protected GUIObject DrawRectangle(Vector2 position, Vector2 size, Color4 color)
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

			int ubo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, ubo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector2.SizeInBytes * 6), textureCoordinates, BufferUsageHint.StreamDraw);

			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			GL.BindVertexArray(0);

			return new GUIObject(vao, pbo, ubo, false, color, null);

		}

		protected GUIObject DrawRectangle(Vector2 position, Vector2 size, Texture texture)
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

			int ubo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, ubo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector2.SizeInBytes * 6), textureCoordinates, BufferUsageHint.StreamDraw);

			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			GL.BindVertexArray(0);

			return new GUIObject(vao, pbo, ubo, true, Color4.White, texture);

		}

		protected GUIObject DrawString(Vector2 position, string text, Font font, Color color)
		{

			Bitmap bitmap = new Bitmap((int) GUI.WIDTH, (int) GUI.HEIGHT, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

			using(Graphics gfx = Graphics.FromImage(bitmap))
			{

				gfx.Clear(Color.Transparent);

				gfx.DrawString(text, font, new SolidBrush(color), new PointF(position.X, position.Y));

			}

			IntPtr data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb).Scan0;

			Texture texture = new Texture();
			texture.LoadTexture(data, bitmap.Width, bitmap.Height);

			return DrawRectangle(new Vector2(0, 0), new Vector2(GUI.WIDTH, GUI.HEIGHT), texture);

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

			set
			{

				this.guiObjects = value;

			}

		}

	}

}