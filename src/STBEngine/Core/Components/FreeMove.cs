using System;

using OpenTK;
using OpenTK.Input;

using STBEngine.Core.Event;

namespace STBEngine.Core.Components
{

	public class FreeMove : Component
	{

		private float speed;

		private bool guiOpened;

		public FreeMove()
		{

			speed = 0.5f;

			guiOpened = false;

		}

		public override void Initialize()
		{

			parent.Engine.EventHandler.Subscribe(OnEvent);

		}

		public override void Update()
		{

			if(!guiOpened)
			{

				if(Input.GetKey(Key.W))
				{

					parent.Transformation.Translate(parent.Engine.RenderingEngine.Camera.Forward, speed);

				}

				if(Input.GetKey(Key.S))
				{

					parent.Transformation.Translate(parent.Engine.RenderingEngine.Camera.Back, speed);

				}

				if(Input.GetKey(Key.A))
				{

					parent.Transformation.Translate(parent.Engine.RenderingEngine.Camera.Left, speed);

				}

				if(Input.GetKey(Key.D))
				{

					parent.Transformation.Translate(parent.Engine.RenderingEngine.Camera.Right, speed);

				}

				if(Input.GetKey(Key.LShift))
				{

					parent.Transformation.Translate(parent.Engine.RenderingEngine.Camera.Down, speed);

				}

				if(Input.GetKey(Key.Space))
				{

					parent.Transformation.Translate(parent.Engine.RenderingEngine.Camera.Up, speed);

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

		public float Speed
		{

			get
			{

				return speed;

			}
			set
			{

				this.speed = value;

			}

		}

	}

}