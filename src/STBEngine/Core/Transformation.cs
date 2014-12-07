using System;

using OpenTK;

namespace STBEngine.Core
{

	public class Transformation
	{

		private Vector3 position;
		private Quaternion rotation;
		private Vector3 scale;

		public Transformation()
		{

			position = new Vector3(0f, 0f, 0f);
			rotation = new Quaternion(0f, 0f, 0f, 1f);
			scale = new Vector3(1f, 1f, 1f);

		}

		public void Translate(Vector3 direction, float distance)
		{

			position += direction * distance;

		}

		public void Rotate(Vector3 axis, float angle)
		{

			rotation *= Quaternion.FromAxisAngle(axis, MathHelper.DegreesToRadians(angle));

		}

		public void Enlarge(Vector3 size)
		{

			scale += size;

		}

		public Matrix4 GetTransformation()
		{

			return Matrix4.CreateTranslation(position) * Matrix4.CreateFromQuaternion(rotation) * Matrix4.CreateScale(scale);

		}

		public Vector3 Position
		{

			get
			{

				return position;

			}
			set
			{

				this.position = value;

			}

		}

		public Quaternion Rotation
		{

			get
			{

				return rotation;

			}
			set
			{

				this.rotation = value;

			}

		}

		public Vector3 Scale
		{

			get
			{

				return scale;

			}
			set
			{

				this.scale = value;

			}

		}

	}

}