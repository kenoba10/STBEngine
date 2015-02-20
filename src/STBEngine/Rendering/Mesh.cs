using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using STBEngine.Rendering.Models;

namespace STBEngine.Rendering
{

	public class Mesh
	{

		private int vao;

		private int pbo;
		private int ubo;
		private int nbo;
		private int tbo;

		private int ibo;

		private uint vertexCount;
		private uint indexCount;

		public Mesh(Model model)
		{

			vertexCount = model.VertexCount;
			indexCount = model.IndexCount;

			vao = GL.GenVertexArray();

			GL.BindVertexArray(vao);

			pbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, pbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector3.SizeInBytes * vertexCount), Vertex.CreateVertexArray(model.Vertices.ToArray()), BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			ubo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, ubo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector2.SizeInBytes * vertexCount), Vertex.CreateTextureCoordinateArray(model.Vertices.ToArray()), BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			nbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, nbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector3.SizeInBytes * vertexCount), Vertex.CreateNormalArray(model.Vertices.ToArray()), BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			tbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, tbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector3.SizeInBytes * vertexCount), Vertex.CreateTangentArray(model.Vertices.ToArray()), BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(3, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			ibo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);

			GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(4 * indexCount), model.Indicies.ToArray(), BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

			GL.BindVertexArray(0);

		}

		public void Draw()
		{

			GL.BindVertexArray(vao);

			GL.EnableVertexAttribArray(0);
			GL.EnableVertexAttribArray(1);
			GL.EnableVertexAttribArray(2);
			GL.EnableVertexAttribArray(3);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);

			GL.DrawElements(PrimitiveType.Triangles, (int) indexCount, DrawElementsType.UnsignedInt, 0);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

			GL.DisableVertexAttribArray(3);
			GL.DisableVertexAttribArray(2);
			GL.DisableVertexAttribArray(1);
			GL.DisableVertexAttribArray(0);

			GL.BindVertexArray(0);

		}

		public void Delete()
		{

			GL.DeleteBuffer(ibo);

			GL.DeleteBuffer(tbo);
			GL.DeleteBuffer(nbo);
			GL.DeleteBuffer(ubo);
			GL.DeleteBuffer(pbo);

			GL.DeleteVertexArray(vao);

		}

	}

}