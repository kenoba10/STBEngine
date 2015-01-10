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

				this.vertices.Add(vertex);

			}

			foreach(Index index in indicies)
			{

				this.indicies.Add(index);

			}

			Model.CalculateNormals(this.vertices, this.indicies);

		}

		public void AddVertex(Vertex vertex)
		{

			vertices.Add(vertex);

		}

		public void AddIndex(Index index)
		{

			indicies.Add(index);

		}

		public static void CalculateNormals(List<Vertex> vertices, List<Index> indicies)
		{

			for(int i = 0; i < indicies.Count; i += 3)
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