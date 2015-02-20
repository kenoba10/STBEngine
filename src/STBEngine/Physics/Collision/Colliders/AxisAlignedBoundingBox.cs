using System;

using OpenTK;

using STBEngine.Core;
using STBEngine.Rendering;
using STBEngine.Rendering.Models;

namespace STBEngine.Physics.Collision.Colliders
{

	public class AxisAlignedBoundingBox : Collider
	{

		private Vector3 minimumExtent;
		private Vector3 maximumExtent;

		private Vector3 minimumExtentTransformed;
		private Vector3 maximumExtentTransformed;

		public AxisAlignedBoundingBox(Vector3 minimumExtent, Vector3 maximumExtent)
		{

			this.minimumExtent = minimumExtent;
			this.maximumExtent = maximumExtent;

			minimumExtentTransformed = new Vector3(0f, 0f, 0f);
			maximumExtentTransformed = new Vector3(0f, 0f, 0f);

		}

		public override Intersection Intersect(Collider collider)
		{

			if(collider.Type == ColliderType.AxisAlignedBoundingBox)
			{

				return Detection.Intersect(this, (AxisAlignedBoundingBox) collider);

			}

			return new Intersection(false, new Vector3(0f, 0f, 0f));

		}

		public override void Transform(Transformation transformation)
		{

			minimumExtentTransformed = minimumExtent + transformation.Position;
			maximumExtentTransformed = maximumExtent + transformation.Position;
			minimumExtentTransformed *= transformation.Scale;
			maximumExtentTransformed *= transformation.Scale;

		}

		public static AxisAlignedBoundingBox GenerateAABB(Model model)
		{

			Vector3 minimumExtent = new Vector3(Single.MaxValue, Single.MaxValue, Single.MaxValue);
			Vector3 maximumExtent = new Vector3(-Single.MaxValue, -Single.MaxValue, -Single.MaxValue);

			foreach(Vertex vertex in model.Vertices)
			{

				minimumExtent.X = Math.Min(minimumExtent.X, vertex.Position.X);
				minimumExtent.Y = Math.Min(minimumExtent.Y, vertex.Position.Y);
				minimumExtent.Z = Math.Min(minimumExtent.Z, vertex.Position.Z);

				maximumExtent.X = Math.Max(maximumExtent.X, vertex.Position.X);
				maximumExtent.Y = Math.Max(maximumExtent.Y, vertex.Position.Y);
				maximumExtent.Z = Math.Max(maximumExtent.Z, vertex.Position.Z);

			}

			return new AxisAlignedBoundingBox(minimumExtent, maximumExtent);

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