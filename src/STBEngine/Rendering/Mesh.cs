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
		private int nbo;

		private int ibo;

		private uint vertexCount;
		private uint indexCount;

		public void AddVertices(Vertex[] vertices, Index[] indicies)
		{

			calculateNormals(vertices, indicies);

			vertexCount = (uint) vertices.Length;
			indexCount = (uint) indicies.Length;

			vao = GL.GenVertexArray();

			GL.BindVertexArray(vao);

			vbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector3.SizeInBytes * vertexCount), Vertex.CreateVertexArray(vertices), BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			tbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, tbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector2.SizeInBytes * vertexCount), Vertex.CreateTextureCoordinateArray(vertices), BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vector2.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			nbo = GL.GenBuffer();

			GL.BindBuffer(BufferTarget.ArrayBuffer, nbo);

			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(Vector3.SizeInBytes * vertexCount), Vertex.CreateNormalArray(vertices), BufferUsageHint.StaticDraw);

			GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vector3.SizeInBytes, 0);

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
			GL.EnableVertexAttribArray(2);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo);

			GL.DrawElements(PrimitiveType.Triangles, (int) indexCount, DrawElementsType.UnsignedInt, 0);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

			GL.DisableVertexAttribArray(2);
			GL.DisableVertexAttribArray(1);
			GL.DisableVertexAttribArray(0);

			GL.BindVertexArray(0);

		}

		public void RemoveVertices()
		{

			GL.DeleteBuffer(ibo);

			GL.DeleteBuffer(nbo);
			GL.DeleteBuffer(tbo);
			GL.DeleteBuffer(vbo);

			GL.DeleteVertexArray(vao);

		}

		private void calculateNormals(Vertex[] vertices, Index[] indicies)
		{

			for(uint i = 0; i < indicies.Length; i += 3)
			{

				uint i0 = indicies[i].Index_;
				uint i1 = indicies[i + 1].Index_;
				uint i2 = indicies[i + 2].Index_;

				Vector3 v1 = vertices[i1].Position - vertices[i0].Position;
				Vector3 v2 = vertices[i2].Position - vertices[i0].Position;

				Vector3 normal = Vector3.Cross(v1, v2).Normalized();

				vertices[i0].Normal = vertices[i0].Normal + normal;
				vertices[i1].Normal = vertices[i1].Normal + normal;
				vertices[i2].Normal = vertices[i2].Normal + normal;

			}

			foreach(Vertex vertex in vertices)
			{

				vertex.Normal.Normalize();

			}

		}

	}

}