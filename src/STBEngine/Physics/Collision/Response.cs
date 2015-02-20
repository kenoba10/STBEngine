using System;

using OpenTK;

using STBEngine.Core;

namespace STBEngine.Physics.Collision
{

	public static class Response
	{

		public static void Log(Entity entity, Intersection intersection)
		{

			Console.WriteLine(intersection.Distance);

		}

		public static void Move(Entity entity, Intersection intersection)
		{

			float maxValue = Math.Max(intersection.Distance.X, Math.Max(intersection.Distance.Y, intersection.Distance.Z));

			Vector3 direction = new Vector3(0f, 0f, 0f);
			float distance = 1f;

			if(intersection.Distance.X == maxValue)
			{

				direction.X = maxValue;

				if((entity.Transformation.Position - direction).X > 0f)
				{

					distance = -1f;

				}

			}
			else if(intersection.Distance.Y == maxValue)
			{

				direction.Y = maxValue;

				if((entity.Transformation.Position - direction).Y > 0f)
				{

					distance = -1f;

				}

			}
			else if(intersection.Distance.Z == maxValue)
			{

				direction.Z = maxValue;

				if((entity.Transformation.Position - direction).Z > 0f)
				{

					distance = -1f;

				}

			}

			entity.Transformation.Translate(direction, distance);

		}

		public static void Bounce(Entity entity, Intersection intersection)
		{

			Move(entity, intersection);

			entity.Velocity = -entity.Velocity;

		}

	}

}