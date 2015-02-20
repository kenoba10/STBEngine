using System;

using OpenTK;
using OpenTK.Input;

using STBEngine.Core.Event;

namespace STBEngine.Core.Components
{

	public class FreeLook : Component
	{

		private float sensitivity;

		private bool locked;
		private bool guiOpened;

		public FreeLook()
		{

			sensitivity = 0.25f;

			locked = false;
			guiOpened = false;

		}

		public override void Initialize()
		{

			parent.Engine.EventHandler.Subscribe(OnEvent);

		}

		public override void Update()
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

			if(locked && !guiOpened)
			{

				Vector2 deltaPosition = Input.GetMousePosition();

				bool rotateX = deltaPosition.Y != 0;
				bool rotateY = deltaPosition.X != 0;

				if(rotateX)
				{

					parent.Transformation.Rotate(parent.Engine.RenderingEngine.Camera.Right, deltaPosition.Y * sensitivity);

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

		public override void Terminate()
		{

			parent.Engine.EventHandler.Unsubscribe(OnEvent);

		}

		public void OnEvent(STBEventArgs e)
		{

			if(e.Event == "openGUI")
			{

				guiOpened = true;

			}
			else if(e.Event == "closeGUI")
			{

				guiOpened = false;

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