using System;

using OpenTK;
using OpenTK.Input;

namespace STBEngine.Core.Components
{

	public class FreeLook : Component
	{

		private float sensitivity;

		private bool locked;

		public FreeLook()
		{

			sensitivity = 0.25f;

			locked = false;

		}

		public override void Simulate()
		{

			if(Input.GetKeyDown(Key.P))
			{

				Input.LockMouse(true);

				locked = true;

				Input.SetMousePosition();

			}

			if(Input.GetKeyDown(Key.Escape))
			{

				Input.LockMouse(false);

				locked = false;

			}

			if(locked)
			{

				Vector2 deltaPosition = Input.GetMousePosition();

				bool rotateX = deltaPosition.Y != 0;
				bool rotateY = deltaPosition.X != 0;

				if(rotateX)
				{

					parent.Transformation.Rotate(CoreEngine.Instance.RenderingEngine.Camera.Right, deltaPosition.Y * sensitivity);

				}

				if(rotateY)
				{

					parent.Transformation.Rotate(new Vector3(0f, 1f, 0f), deltaPosition.X * sensitivity);

				}

				if(rotateX || rotateY)
				{

					Input.SetMousePosition();

				}

			}

		}

		public float Sensitivity
		{

			get
			{

				return sensitivity;

			}
			set
			{

				this.sensitivity = value;

			}

		}

	}

}