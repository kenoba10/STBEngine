using System;

using OpenTK;
using OpenTK.Input;

using STBEngine.Core.Event;

namespace STBEngine.Core.Components
{

	public class FreeMove : Component
	{

		private bool guiOpened;

		public FreeMove()
		{

			guiOpened = false;

		}

		public override void Initialize()
		{

			CoreEngine.Instance.EventHandler.Subscribe(OnEvent);

		}

		public override void Simulate()
		{

			if(!guiOpened)
			{

				if(Input.GetKey(Key.W))
				{

					parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Forward;

				}

				if(Input.GetKey(Key.S))
				{

					parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Back;

				}

				if(Input.GetKey(Key.A))
				{

					parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Left;

				}

				if(Input.GetKey(Key.D))
				{

					parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Right;

				}

				if(Input.GetKey(Key.LShift))
				{

					parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Down;

				}

				if(Input.GetKey(Key.Space))
				{

					parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Up;

				}

			}

		}

		public override void Terminate()
		{

			CoreEngine.Instance.EventHandler.Unsubscribe(OnEvent);

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

	}

}