using System;

using OpenTK;
using OpenTK.Input;

namespace STBEngine.Core.Components
{

	public class FreeMove : Component
	{

		private float speed;

		public FreeMove()
		{

			speed = 0.25f;

		}

		public override void Simulate()
		{

			if(Input.GetKey(Key.W))
			{

				parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Forward * speed;

			}

			if(Input.GetKey(Key.S))
			{

				parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Back * speed;

			}

			if(Input.GetKey(Key.A))
			{

				parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Left * speed;

			}

			if(Input.GetKey(Key.D))
			{

				parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Right * speed;

			}

			if(Input.GetKey(Key.LShift))
			{

				parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Down * speed;

			}

			if(Input.GetKey(Key.Space))
			{

				parent.Velocity += CoreEngine.Instance.RenderingEngine.Camera.Up * speed;

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