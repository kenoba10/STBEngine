using System;

using OpenTK;

namespace STBEngine.Physics.Collision
{

	public class Intersection
	{

		private bool intersecting;
		private Vector3 distance;

		public Intersection(bool intersecting, Vector3 distance)
		{

			this.intersecting = intersecting;
			this.distance = distance;

		}

		public bool Intersecting
		{

			get
			{

				return intersecting;

			}

		}

		public Vector3 Distance
		{

			get
			{

				return distance;

			}

		}

	}

}