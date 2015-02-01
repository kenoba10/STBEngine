using System;

using OpenTK;

using STBEngine.Physics.Collision.Colliders;
using STBEngine.Rendering;

namespace STBEngine.Physics.Collision
{

	public static class Detection
	{

		public static Intersection Intersect(AxisAlignedBoundingBox bb1, AxisAlignedBoundingBox bb2)
		{

			Vector3 distance1 = bb2.MinimumExtent - bb1.MaximumExtent;
			Vector3 distance2 = bb1.MinimumExtent - bb2.MaximumExtent;

			Vector3 distance = Vector3.ComponentMax(distance1, distance2);
			float maxDistance = Math.Max(distance.X, Math.Max(distance.Y, distance.Z));

			return new Intersection(maxDistance < 0f, distance);

		}

		public static Intersection Intersect(BoundingSphere bs1, BoundingSphere bs2)
		{

			float distance = (bs1.Center - bs2.Center).Length - (bs1.Radius + bs2.Radius);

			return new Intersection(distance < 0f, (bs1.Center - bs2.Center) * distance);

		}

		public static Intersection Intersect(BoundingSphere bs1, BoundingPolygon bp2)
		{

			Vector3 closestPoint = new Vector3(Single.MaxValue, Single.MaxValue, Single.MaxValue);

			foreach(Vertex vertex in bp2.Model.Vertices)
			{

				if(Vector3.Min(bs1.Center - vertex.Position, bs1.Center - closestPoint) == bs1.Center - vertex.Position)
				{

					closestPoint = vertex.Position;

				}

			}

			Vector3 normal = (closestPoint - bs1.Center).Normalized();

			float min1 = Single.MaxValue;
			float max1 = -Single.MaxValue;

			float min2 = Single.MaxValue;
			float max2 = -Single.MaxValue;

			foreach(Vertex vertex in bp2.Model.Vertices)
			{

				float t = Vector3.Dot(normal, vertex.Position);

				min1 = Math.Min(min1, t);
				max1 = Math.Max(max1, t);

			}

			min2 = Vector3.Dot(normal, bs1.Center) - bs1.Radius;
			max2 = Vector3.Dot(normal, bs1.Center) + bs1.Radius;

			float distance = Math.Max(min1 - max2, min2 - max1);

			return new Intersection(distance < 0f, normal * distance);

		}

		public static Intersection Intersect(BoundingPolygon bp1, BoundingPolygon bp2)
		{

			Vector3 shortestDistance = new Vector3(Single.MaxValue, Single.MaxValue, Single.MaxValue);

			foreach(Vertex axis in bp1.Model.Vertices)
			{

				Vector3 normal = axis.Normal;

				float min1 = Single.MaxValue;
				float max1 = -Single.MaxValue;

				float min2 = Single.MaxValue;
				float max2 = -Single.MaxValue;

				foreach(Vertex vertex in bp1.Model.Vertices)
				{

					float t = Vector3.Dot(normal, vertex.Position);

					min1 = Math.Min(min1, t);
					max1 = Math.Max(max1, t);

				}

				foreach(Vertex vertex in bp2.Model.Vertices)
				{

					float t = Vector3.Dot(normal, vertex.Position);

					min2 = Math.Min(min2, t);
					max2 = Math.Max(max2, t);

				}

				float distance = Math.Max(min1 - max2, min2 - max1);

				shortestDistance = Vector3.ComponentMin(shortestDistance, normal * distance);

				if(distance > 0f)
				{

					return new Intersection(false, shortestDistance);

				}

			}

			return new Intersection(true, shortestDistance);

		}

	}

}