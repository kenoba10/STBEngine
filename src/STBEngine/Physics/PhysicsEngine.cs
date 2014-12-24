using System;

using STBEngine.Core;
using STBEngine.Physics.Colliders;

namespace STBEngine.Physics
{

	public class PhysicsEngine
	{

		public void Initialize()
		{

		}

		public void Update()
		{

		}

		public void Simulate(Entity entity)
		{

			entity.Simulate();

			entity.Collider.Transform(entity.Transformation);

			foreach(Entity otherEntity in CoreEngine.Instance.Entities)
			{

				Intersection intersection = entity.Collider.Intersect(otherEntity.Collider);

				Console.WriteLine(intersection.Intersecting);
				Console.WriteLine(intersection.Distance);

			}

		}

		public void Terminate()
		{

		}

	}

}