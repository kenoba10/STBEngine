using System;
using System.Collections.Generic;

using STBEngine.Core;
using STBEngine.Physics.Collision;
using STBEngine.Physics.Collision.Colliders;
using STBEngine.Physics.Components;

namespace STBEngine.Physics
{

	public class PhysicsEngine
	{

		private CoreEngine engine;

		private List<PhysicsComponent> components;

		public PhysicsEngine(CoreEngine engine)
		{

			this.engine = engine;

			components = new List<PhysicsComponent>();

		}

		public void Initialize()
		{

		}

		public void Update()
		{

			foreach(PhysicsComponent component in components)
			{

				component.Update();

			}

		}

		public void Simulate()
		{

			foreach(PhysicsComponent component in components)
			{

				component.Simulate();

			}

		}

		public void Terminate()
		{

		}

		public void Simulate(Entity entity)
		{

			entity.Simulate();

			foreach(PhysicsComponent component in components)
			{

				component.Simulate(entity);

			}

			entity.Transformation.Translate(entity.Velocity, 1f);

			entity.AABB.Transform(entity.Transformation);

			foreach(Collider collider in entity.Colliders)
			{
				collider.Transform(entity.Transformation);

				foreach(Entity otherEntity in engine.Entities)
				{

					if(otherEntity == entity)
					{

						continue;

					}

					otherEntity.AABB.Transform(otherEntity.Transformation);

					if(entity.AABB.Intersect(otherEntity.AABB).Intersecting)
					{

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

		}

		public void AddComponent(PhysicsComponent component)
		{

			components.Add(component);

		}

		public void RemoveComponent(PhysicsComponent component)
		{

			components.Remove(component);

		}

	}

}