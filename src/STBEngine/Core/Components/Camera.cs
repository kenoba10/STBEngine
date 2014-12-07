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

		public Matrix4 Projection
		{

			get
			{

				return projection;

			}

		}

	}

}