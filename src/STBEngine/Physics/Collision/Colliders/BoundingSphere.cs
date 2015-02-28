using System;

using OpenTK;

using STBEngine.Core;

namespace STBEngine.Physics.Collision.Colliders
{


	//WARNING: Bounding Spheres and Polygons Are VERY BUGGY.  Use At Own Risk!
	public class BoundingSphere : Collider
	{

		private Vector3 center;
		private float radius;

		private Vector3 centerTransformed;

		public BoundingSphere(Vector3 center, float radius)
		{

			this.center = center;
			this.radius = radius;

			centerTransformed = center;

		}

		public override Intersection Intersect(Collider collider)
		{

			if(collider.Type == ColliderType.BoundingSphere)
			{

				return Detection.Intersect(this, (BoundingSphere) collider);

			}
			else if(collider.Type == ColliderType.BoundingPolygon)
			{

				return Detection.Intersect(this, (BoundingPolygon) collider);

			}

			return new Intersection(false, new Vector3(0f, 0f, 0f));

		}

		public override void Transform(Transformation transformation)
		{

			centerTransformed = center + transformation.Position;

		}

		public Vector3 Center
		{

			get
			{

				return centerTransformed;

			}
			set
			{

				this.center = value;

			}

		}

		public float Radius
		{

			get
			{

				return radius;

			}
			set
			{

				this.radius = value;

			}

		}

		public override ColliderType Type
		{

			get
			{

				return ColliderType.BoundingSphere;

			}

		}

	}

}