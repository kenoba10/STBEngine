using System;

using OpenTK;

using STBEngine.Core;

namespace STBEngine.Physics.Colliders
{

	public class AxisAlignedBoundingBox : Collider
	{

		private Vector3 minimumExtent;
		private Vector3 maximumExtent;

		private Vector3 minimumExtentTransformed;
		private Vector3 maximumExtentTransformed;

		public AxisAlignedBoundingBox()
		{

			minimumExtent = new Vector3(-1f, -1f, -1f);
			maximumExtent = new Vector3(1f, 1f, 1f);

			minimumExtentTransformed = new Vector3(-1f, -1f, -1f);
			maximumExtentTransformed = new Vector3(1f, 1f, 1f);

		}

		public override Intersection Intersect(Collider collider)
		{

			if(collider.Type == ColliderType.AxisAlignedBoundingBox)
			{

				AxisAlignedBoundingBox aabb = (AxisAlignedBoundingBox) collider;

				Vector3 distance1 = aabb.MaximumExtent - maximumExtentTransformed;
				Vector3 distance2 = minimumExtentTransformed - aabb.MaximumExtent;

				Vector3 distance = Vector3.Max(distance1, distance2);

				float maxDistance = Math.Max(distance.X, Math.Max(distance.Y, distance.Z));

				return new Intersection(maxDistance < 0, distance);

			}

			return new Intersection(false, new Vector3(0f, 0f, 0f));

		}

		public override void Transform(Transformation transformation)
		{

			minimumExtentTransformed = minimumExtent + transformation.Position;
			maximumExtentTransformed = maximumExtent + transformation.Position;

		}

		public Vector3 MinimumExtent
		{

			get
			{

				return minimumExtentTransformed;

			}
			set
			{

				this.minimumExtent = value;

			}

		}

		public Vector3 MaximumExtent
		{

			get
			{

				return maximumExtentTransformed;

			}
			set
			{

				this.maximumExtent = value;

			}

		}

		public override ColliderType Type
		{

			get
			{

				return ColliderType.AxisAlignedBoundingBox;

			}

		}

	}

}