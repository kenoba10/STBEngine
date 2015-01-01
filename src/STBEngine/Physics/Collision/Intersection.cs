using System;

using OpenTK;

namespace STBEngine.Physics.Collision
{

	public class Intersection
	{

		private bool intersecting;
		private Vector3 direction;

		public Intersection(bool intersecting, Vector3 direction)
		{

			this.intersecting = intersecting;
			this.direction = direction;

		}

		public bool Intersecting
		{

			get
			{

				return intersecting;

			}

		}

		public Vector3 Direction
		{

			get
			{

				return direction;

			}

		}

		public float Distance
		{

			get
			{

				return direction.Length;

			}

		}

	}

}