using System;
using System.Collections.Generic;

using OpenTK;

namespace STBEngine.Rendering.Models
{

	public class Model
	{

		private List<Vertex> vertices;
		private List<Index> indicies;

		public Model()
		{

			vertices = new List<Vertex>();
			indicies = new List<Index>();

		}

		public Model(Vertex[] vertices, Index[] indicies)
		{

			this.vertices = new List<Vertex>();
			this.indicies = new List<Index>();

			foreach(Vertex vertex in vertices)
			{

				AddVertex(vertex);

			}

			foreach(Index index in indicies)
			{

				AddIndex(index);

			}

			CalculateNormals();
			CalculateTangents();

		}

		public void AddVertex(Vertex vertex)
		{

			vertices.Add(vertex);

		}

		public void AddIndex(Index index)
		{

			indicies.Add(index);

		}

		public void CalculateNormals()
		{

			Vertex[] vertices = this.vertices.ToArray();
			Index[] indicies = this.indicies.ToArray();

			for(int i = 0; i < indicies.Length; i += 3)
			{

				int i0 = (int) indicies[i].Index_;
				int i1 = (int) indicies[i + 1].Index_;
				int i2 = (int) indicies[i + 2].Index_;

				Vector3 v1 = vertices[i1].Position - vertices[i0].Position;
				Vector3 v2 = vertices[i2].Position - vertices[i0].Position;

				Vector3 normal = Vector3.Cross(v1, v2).Normalized();

				vertices[i0].Normal += normal;
				vertices[i1].Normal += normal;
				vertices[i2].Normal += normal;

			}

			foreach(Vertex vertex in vertices)
			{

				vertex.Normal.Normalize();

			}

			this.vertices.Clear();
			this.indicies.Clear();

			foreach(Vertex vertex in vertices)
			{

				AddVertex(vertex);

			}

			foreach(Index index in indicies)
			{

				AddIndex(index);

			}

		}

		public void CalculateTangents()
		{

			Vertex[] vertices = this.vertices.ToArray();
			Index[] indicies = this.indicies.ToArray();

			for(int i = 0; i < indicies.Length; i += 3)
			{

				int i0 = (int) indicies[i].Index_;
				int i1 = (int) indicies[i + 1].Index_;
				int i2 = (int) indicies[i + 2].Index_;

				Vector3 edge1 = vertices[i1].Position - vertices[i0].Position;
				Vector3 edge2 = vertices[i2].Position - vertices[i0].Position;

				float deltaU1 = vertices[i1].TextureCoordinates.X - vertices[i0].TextureCoordinates.X;
				float deltaV1 = vertices[i1].TextureCoordinates.Y - vertices[i0].TextureCoordinates.Y;
				float deltaU2 = vertices[i2].TextureCoordinates.X - vertices[i0].TextureCoordinates.X;
				float deltaV2 = vertices[i2].TextureCoordinates.Y - vertices[i0].TextureCoordinates.Y;

				float dividend = deltaU1 * deltaV2 - deltaU2 * deltaV1;

				float f = dividend == 0f ? 0f : 1f / dividend;

				Vector3 tangent = new Vector3(0f, 0f, 0f);

				tangent.X = f * (deltaV2 * edge1.X - deltaV1 * edge2.X);
				tangent.Y = f * (deltaV2 * edge1.Y - deltaV1 * edge2.Y);
				tangent.Z = f * (deltaV2 * edge1.Z - deltaV1 * edge2.Z);

				vertices[i0].Tangent += tangent;
				vertices[i1].Tangent += tangent;
				vertices[i2].Tangent += tangent;

			}

			foreach(Vertex vertex in vertices)
			{

				vertex.Tangent.Normalize();

			}

			this.vertices.Clear();
			this.indicies.Clear();

			foreach(Vertex vertex in vertices)
			{

				AddVertex(vertex);

			}

			foreach(Index index in indicies)
			{

				AddIndex(index);

			}

		}

		public List<Vertex> Vertices
		{

			get
			{

				return vertices;

			}

		}

		public List<Index> Indicies
		{

			get
			{

				return indicies;

			}

		}

		public uint VertexCount
		{

			get
			{

				return (uint) vertices.Count;

			}

		}

		public uint IndexCount
		{

			get
			{

				return (uint) indicies.Count;

			}

		}

	}

}