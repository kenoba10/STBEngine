using System;

using STBEngine.Core;
using STBEngine.Physics.Collision;

namespace STBEngine.Physics
{

	public class PhysicsEngine
	{

		private CoreEngine engine;

		public PhysicsEngine(CoreEngine engine)
		{

			this.engine = engine;

		}

		public void Initialize()
		{

		}

		public void Update()
		{

		}

		public void Simulate(Entity entity)
		{

			entity.Simulate();

			foreach(Collider collider in entity.Colliders)
			{

				collider.Transform(entity.Transformation);

				foreach(Entity otherEntity in engine.Entities)
				{

					if(otherEntity == entity)
					{

						continue;

					}

					foreach(Collider otherCollider in otherEntity.Colliders)
					{

						otherCollider.Transform(otherEntity.Transformation);

						Intersection intersection = collider.Intersect(otherCollider);

						if(intersection.Intersecting)
						{

							collider.Response(entity, intersection);

						}

					}

				}

			}

		}

		public void Terminate()
		{

		}

	}

}