using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace STBEngine.Rendering
{

	public class Mesh
	{

		private int vao;

		private int vbo;
		private int tbo;
		private int ibo;

		private uint vertexCount;
		private uint indexCount;

		public void AddVertices(Vertex[] vertices, Index[] indicies)
		{

			vertexCount = (uint) vertices.Length;
			indexCount = (uint) indicies.Length;

			vao = GL.GenVertexArray();

			GL.BindVertexArray(vao);

			vbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector3.SizeInBytes * vertexCount), Vertex.CreateVertexArray(vertices), BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, (int) Vector3.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			tbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, tbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector2.SizeInBytes * vertexCount), Vertex.CreateTextureCoordinateArray(vertices), BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, (int) Vector2.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			ibo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);

			GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(4 * indexCount), indicies, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

			GL.BindVertexArray(0);

		}

		public void Draw()
		{

			GL.BindVertexArray(vao);

			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);

			GL.DrawElements(PrimitiveType.Triangles, (int) indexCount, DrawElementsType.UnsignedInt, 0);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

			GL.DisableVertexAttribArray(1);
			GL.DisableVertexAttribArray(0);

			GL.BindVertexArray(0);

		}

		public void RemoveVertices()
		{

			GL.DeleteBuffer(ibo);
			GL.DeleteBuffer(vbo);

			GL.DeleteVertexArray(vao);

		}

	}

}