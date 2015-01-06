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

			parent.Engine.EventHandler.Subscribe(OnEvent);

		}

		public override void Simulate()
		{

			if(!guiOpened)
			{

				if(Input.GetKey(Key.W))
				{

					parent.Velocity += parent.Engine.RenderingEngine.Camera.Forward;

				}

				if(Input.GetKey(Key.S))
				{

					parent.Velocity += parent.Engine.RenderingEngine.Camera.Back;

				}

				if(Input.GetKey(Key.A))
				{

					parent.Velocity += parent.Engine.RenderingEngine.Camera.Left;

				}

				if(Input.GetKey(Key.D))
				{

					parent.Velocity += parent.Engine.RenderingEngine.Camera.Right;

				}

				if(Input.GetKey(Key.LShift))
				{

					parent.Velocity += parent.Engine.RenderingEngine.Camera.Down;

				}

				if(Input.GetKey(Key.Space))
				{

					parent.Velocity += parent.Engine.RenderingEngine.Camera.Up;

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

	}

}