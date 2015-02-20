using System;

using OpenTK;

namespace STBEngine.Core.Components
{

	public class CameraOrthographic : Camera
	{

		public CameraOrthographic()
		{

		}

		public override void Update()
		{

			base.Update();

			projection =  Matrix4.CreateTranslation(-parent.Transformation.Position) * Matrix4.CreateFromQuaternion(parent.Transformation.Rotation) * Matrix4.CreateOrthographicOffCenter(0f, width, height, 0f, zNear, zFar);

		}

	}

}