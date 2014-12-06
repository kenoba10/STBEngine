using System;

using OpenTK;

namespace STBEngine.Rendering
{

	public struct Vertex
	{

		public static readonly uint SIZE = 3;
		public static readonly uint SIZE_IN_BYTES = SIZE * 4;

		private Vector3 position;

		public Vertex(Vector3 position)
		{

			this.position = position;

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

	}

}