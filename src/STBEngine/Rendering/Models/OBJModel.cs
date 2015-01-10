using System;
using System.IO;
using System.Collections.Generic;

using OpenTK;

namespace STBEngine.Rendering.Models
{

	public struct OBJIndex
	{

		private int positionIndex;
		private int textureCoordinatesIndex;
		private int normalIndex;

		public OBJIndex(int positionIndex, int textureCoordinatesIndex, int normalIndex)
		{

			this.positionIndex = positionIndex;
			this.textureCoordinatesIndex = textureCoordinatesIndex;
			this.normalIndex = normalIndex;

		}

		public int PositionIndex
		{

			get
			{

				return positionIndex;

			}

		}

		public int TextureCoordinateIndex
		{

			get
			{

				return textureCoordinatesIndex;

			}

		}

		public int NormalIndex
		{

			get
			{

				return normalIndex;

			}

		}

	}

	public class OBJModel : Model
	{

		public OBJModel(Stream stream)
		{

			List<Vector3> positions = new List<Vector3>();
			List<Vector2> textureCoordinates = new List<Vector2>();
			List<Vector3> normals = new List<Vector3>();

			List<OBJIndex> indicies = new List<OBJIndex>();

			using(StreamReader reader = new StreamReader(stream))
			{

				string line;

				while((line = reader.ReadLine()) != null)
				{

					string[] tokens = line.Split(' ');

					if(tokens[0] == "v")
					{

						positions.Add(new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])));

					}
					else if(tokens[0] == "vt")
					{

						textureCoordinates.Add(new Vector2(float.Parse(tokens[1]), float.Parse(tokens[2])));

					}
					else if(tokens[0] == "vn")
					{

						normals.Add(new Vector3(float.Parse(tokens[1]), float.Parse(tokens[2]), float.Parse(tokens[3])));

					}
					else if(tokens[0] == "f")
					{

						indicies.Add(CalculateIndex(tokens[1]));
						indicies.Add(CalculateIndex(tokens[2]));
						indicies.Add(CalculateIndex(tokens[3]));

					}

				}

			}

			Dictionary<OBJIndex, int> indexMap = new Dictionary<OBJIndex, int>();

			int currentVertexIndex = 0;

			for(int i = 0; i < indicies.Count; i++)
			{

				OBJIndex index = indicies[i];

				Vector3 position;
				Vector2 textureCoordinate;
				Vector3 normal;

				position = positions[index.PositionIndex];

				if(index.TextureCoordinateIndex != -1)
				{

					textureCoordinate = textureCoordinates[index.TextureCoordinateIndex];

				}
				else
				{

					textureCoordinate = new Vector2(0f, 0f);

				}

				if(index.NormalIndex != -1)
				{

					normal = normals[index.NormalIndex];

				}
				else
				{

					normal = new Vector3(0f, 0f, 0f);

				}

				int previousVertexIndex = -1;

				try
				{

					previousVertexIndex = indexMap[index];

				}
				catch(KeyNotFoundException e)
				{
					
				}

				if(previousVertexIndex == -1)
				{

					AddVertex(new Vertex(position, textureCoordinate, normal));
					AddIndex(new Index((uint) currentVertexIndex));

					indexMap.Add(index, currentVertexIndex);
					currentVertexIndex++;

				}
				else
				{

					AddIndex(new Index((uint) previousVertexIndex));

				}

			}

		}

		private OBJIndex CalculateIndex(string token)

		{

			string[] tokens = token.Split('/');

			int vertexIndex = -1;
			int textureCoordinatesIndex = -1;
			int normalIndex = -1;

			vertexIndex = int.Parse(tokens[0]) - 1;

			if(tokens.Length > 1)
			{

				textureCoordinatesIndex = int.Parse(tokens[1]) - 1;

				if(tokens.Length > 2)
				{

					normalIndex = int.Parse(tokens[2]) - 1;

				}

			}

			return new OBJIndex(vertexIndex, textureCoordinatesIndex, normalIndex);

		}

	}

}