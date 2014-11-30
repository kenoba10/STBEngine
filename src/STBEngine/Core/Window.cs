using System;

using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;

namespace STBEngine.Core
{

	public class Window : GameWindow
	{

		public Window(string title) : base(1080, 720, GraphicsMode.Default, title, GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.Default)
		{

		}

		protected override void OnLoad(EventArgs e)
		{

			base.OnLoad(e);

			CoreEngine.Instance.Initialize();

		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{

			base.OnUpdateFrame(e);

			Input.Update();

			CoreEngine.Instance.Update();

		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{

			base.OnRenderFrame(e);

		}

		protected override void OnResize(EventArgs e)
		{

			base.OnResize(e);

		}

		protected override void OnUnload(EventArgs e)
		{

			base.OnUnload(e);

			CoreEngine.Instance.Terminate();

		}

		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{

			base.OnKeyDown(e);

			CoreEngine.Instance.EventHandler.Execute("Key" + e.Key.ToString() + "Down");

		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{

			base.OnKeyPress(e);

			CoreEngine.Instance.EventHandler.Execute("Key" + e.KeyChar + "Press");

		}

		protected override void OnKeyUp(KeyboardKeyEventArgs e)
		{

			base.OnKeyUp(e);

			CoreEngine.Instance.EventHandler.Execute("Key" + e.Key.ToString() + "Up");

		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{

			base.OnMouseDown(e);

			CoreEngine.Instance.EventHandler.Execute("MouseButton" + e.Button.ToString() + "Down");

		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{

			base.OnMouseUp(e);

			CoreEngine.Instance.EventHandler.Execute("MouseButton" + e.Button.ToString() + "Up");

		}

	}

}