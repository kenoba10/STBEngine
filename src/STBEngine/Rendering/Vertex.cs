using System;

using OpenTK;

namespace STBEngine.Rendering
{

	public struct Vertex
	{

		private Vector3 position;
		private Vector2 textureCoordinates;
		private Vector3 normal;

		public Vertex(Vector3 position, Vector2 textureCoordinates) : this(position, textureCoordinates, new Vector3(0f, 0f, 0f))
		{

		}

		public Vertex(Vector3 position, Vector2 textureCoordinates, Vector3 normal)
		{

			this.position = position;
			this.textureCoordinates = textureCoordinates;
			this.normal = normal;

		}

		public static Vector3[] CreateVertexArray(Vertex[] vertices)
		{

			Vector3[] newVertices = new Vector3[vertices.Length];

			for(uint i = 0; i < vertices.Length; i++)
			{

				newVertices[i] = vertices[i].Position;

			}

			return newVertices;

		}

		public static Vector2[] CreateTextureCoordinateArray(Vertex[] vertices)
		{

			Vector2[] newVertices = new Vector2[vertices.Length];

			for(uint i = 0; i < vertices.Length; i++)
			{

				newVertices[i] = vertices[i].TextureCoordinates;

			}

			return newVertices;

		}

		public static Vector3[] CreateNormalArray(Vertex[] vertices)
		{

			Vector3[] newVertices = new Vector3[vertices.Length];

			for(uint i = 0; i < vertices.Length; i++)
			{

				newVertices[i] = vertices[i].Normal;

			}

			return newVertices;

		}

		public Vector3 Position
		{

			get
			{

				return position;

			}
			set
			{

				this.position = value;

			}

		}

		public Vector2 TextureCoordinates
		{

			get
			{

				return textureCoordinates;

			}
			set
			{

				this.textureCoordinates = value;

			}

		}

		public Vector3 Normal
		{

			get
			{

				return normal;

			}
			set
			{

				this.normal = value;

			}

		}

	}

}