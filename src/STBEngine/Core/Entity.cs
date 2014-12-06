using System;

using STBEngine.Rendering;

namespace STBEngine.Core
{

	public class Entity
	{

		private Mesh mesh;
		private Shader shader;

		public Entity()
		{

			mesh = new Mesh();
			shader = new Shader();

		}

		public void Update()
		{

		}

		public Mesh Mesh
		{

			get
			{

				return mesh;

			}

		}

		public Shader Shader
		{

			get
			{

				return shader;

			}

		}

	}

}