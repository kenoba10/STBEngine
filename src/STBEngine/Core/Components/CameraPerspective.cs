using System;

using OpenTK;

namespace STBEngine.Core.Components
{

	public class CameraPerspective : Camera
	{

		private float fov;

		public CameraPerspective()
		{

			fov = 90f;

		}

		public override void Update()
		{

			base.Update();

			projection =  Matrix4.CreateTranslation(-parent.Transformation.Position) * Matrix4.CreateFromQuaternion(parent.Transformation.Rotation) * Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(fov), width / height, zNear, zFar);

		}

		public float FOV
		{

			get
			{

				return fov;

			}
			set
			{

				this.fov = value;

			}

		}

	}

}