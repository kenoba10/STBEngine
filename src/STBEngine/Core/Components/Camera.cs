using System;

using OpenTK;

namespace STBEngine.Core.Components
{

	public class Camera : Component
	{

		protected float width;
		protected float height;
		protected float zNear;
		protected float zFar;

		protected Matrix4 projection;

		public Camera()
		{

			width = 1080;
			height = 720;
			zNear = 0.1f;
			zFar = 1000f;

			projection = Matrix4.Identity;

		}

		public float Width
		{

			get
			{

				return width;

			}
			set
			{

				this.width = value;

			}

		}

		public float Height
		{

			get
			{

				return height;

			}
			set
			{

				this.height = value;

			}

		}

		public float ZNear
		{

			get
			{

				return zNear;

			}
			set
			{

				this.zNear = value;

			}

		}

		public float ZFar
		{

			get
			{

				return zFar;

			}
			set
			{

				this.zFar = value;

			}

		}

		public Vector3 Left
		{

			get
			{

				return Vector3.TransformVector(new Vector3(-1f, 0f, 0f), Matrix4.Transpose(Matrix4.CreateFromQuaternion(parent.Transformation.Rotation)));

			}

		}

		public Vector3 Right
		{

			get
			{

				return Vector3.TransformVector(new Vector3(1f, 0f, 0f), Matrix4.Transpose(Matrix4.CreateFromQuaternion(parent.Transformation.Rotation)));

			}

		}

		public Vector3 Down
		{

			get
			{

				return Vector3.TransformVector(new Vector3(0f, -1f, 0f), Matrix4.Transpose(Matrix4.CreateFromQuaternion(parent.Transformation.Rotation)));

			}

		}

		public Vector3 Up
		{

			get
			{

				return Vector3.TransformVector(new Vector3(0f, 1f, 0f), Matrix4.Transpose(Matrix4.CreateFromQuaternion(parent.Transformation.Rotation)));

			}

		}

		public Vector3 Forward
		{

			get
			{

				return Vector3.TransformVector(new Vector3(0f, 0f, -1f), Matrix4.Transpose(Matrix4.CreateFromQuaternion(parent.Transformation.Rotation)));

			}

		}

		public Vector3 Back
		{

			get
			{

				return Vector3.TransformVector(new Vector3(0f, 0f, 1f), Matrix4.Transpose(Matrix4.CreateFromQuaternion(parent.Transformation.Rotation)));

			}

		}

		public Matrix4 Projection
		{

			get
			{

				return projection;

			}

		}

	}

}