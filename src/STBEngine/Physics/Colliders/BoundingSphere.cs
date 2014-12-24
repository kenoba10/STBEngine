using System;

using OpenTK;

using STBEngine.Core;

namespace STBEngine.Physics.Colliders
{

	public class BoundingSphere : Collider
	{

		private Vector3 center;
		private float radius;

		private Vector3 centerTransformed;

		public BoundingSphere()
		{

			center = new Vector3(0f, 0f, 0f);
			radius = 1f;

			centerTransformed = new Vector3(0f, 0f, 0f);

		}

		public override Intersection Intersect(Collider collider)
		{

			if(collider.Type == ColliderType.BoundingSphere)
			{

				BoundingSphere bs = (BoundingSphere) collider;
				
				Vector3 direction = bs.Center - center;

				float radiusDistance = radius + bs.Radius;
				float centerDistance = direction.Length;

				direction /= centerDistance;

				float distance = centerDistance - radiusDistance;

				return new Intersection(distance < 0, direction * distance);

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