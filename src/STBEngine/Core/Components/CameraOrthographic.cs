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

			projection =  Matrix4.CreateTranslation(-parent.Transformation.Position) * Matrix4.CreateOrthographic(width, height, zNear, zFar);

		}

	}

}