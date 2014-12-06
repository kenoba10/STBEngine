using System;

using OpenTK.Graphics.OpenGL;

namespace STBEngine.Rendering
{

	public class Mesh
	{

		private int vao;

		private int vbo;
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

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vertex.SIZE_IN_BYTES * vertexCount), vertices, BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, (int) Vertex.SIZE_IN_BYTES, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			ibo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);

			GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(Index.SIZE_IN_BYTES * indexCount), indicies, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

			GL.BindVertexArray(0);

		}

		public void Draw()
		{

			GL.BindVertexArray(vao);

			GL.EnableVertexAttribArray(0);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);

			GL.DrawElements(PrimitiveType.Triangles, (int) indexCount, DrawElementsType.UnsignedInt, 0);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

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