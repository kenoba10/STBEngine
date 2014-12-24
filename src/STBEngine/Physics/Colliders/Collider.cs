using System;

using STBEngine.Core;

namespace STBEngine.Physics.Colliders
{

	public enum ColliderType
	{

		AxisAlignedBoundingBox,
		BoundingSphere

	};

	public abstract class Collider
	{

		public abstract Intersection Intersect(Collider collider);
		public abstract void Transform(Transformation transformation);

		public abstract ColliderType Type
		{

			get;

		}

	}

}