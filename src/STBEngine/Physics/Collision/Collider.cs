using System;

using STBEngine.Core;
using STBEngine.Physics.Collision.Response;

namespace STBEngine.Physics.Collision
{

	public enum ColliderType
	{
		
		BoundingSphere

	};

	public delegate void CollisionResponse(Entity entity, Intersection intersection);

	public abstract class Collider
	{

		private CollisionResponse response;

		public Collider()
		{

			response = Respond;

		}

		public void Respond(Entity entity, Intersection intersection)
		{

		}

		public abstract Intersection Intersect(Collider collider);
		public abstract void Transform(Transformation transformation);

		public CollisionResponse Response
		{

			get
			{

				return response;

			}
			set
			{

				this.response = value;

			}

		}

		public abstract ColliderType Type
		{

			get;

		}

	}

}