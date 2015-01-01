using System;

using OpenTK;
using OpenTK.Input;

namespace STBEngine.Core.Components
{

	public class FreeMove : Component
	{

		public override void Simulate()
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

}