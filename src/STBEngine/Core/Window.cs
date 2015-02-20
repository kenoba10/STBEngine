using System;

using OpenTK;
using OpenTK.Audio;
using OpenTK.Graphics;
using OpenTK.Input;

namespace STBEngine.Core
{

	public class Window : GameWindow
	{

		private CoreEngine engine;

		private AudioContext AC;

		public Window(string title, CoreEngine engine) : base(1080, 720, new GraphicsMode(32, 24, 0, 4), title, GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.Default)
		{

			this.engine = engine;

			AC = new AudioContext();

		}

		protected override void OnLoad(EventArgs e)
		{

			base.OnLoad(e);

			AC.MakeCurrent();

			engine.Initialize();

		}

		protected override void OnUpdateFrame(FrameEventArgs e)
		{

			base.OnUpdateFrame(e);

			Input.Update(this);

			engine.Update();

		}

		protected override void OnRenderFrame(FrameEventArgs e)
		{

			base.OnRenderFrame(e);

			engine.RenderingEngine.ClearScreen();

			engine.Render();

			SwapBuffers();

		}

		protected override void OnUnload(EventArgs e)
		{

			base.OnUnload(e);

			engine.Terminate();

			AC.Dispose();

		}

		protected override void OnResize(EventArgs e)
		{

			base.OnResize(e);

			engine.RenderingEngine.ResizeScreen((uint) Width, (uint) Height);

		}

		protected override void OnKeyDown(KeyboardKeyEventArgs e)
		{

			base.OnKeyDown(e);

			engine.EventHandler.Execute("Key" + e.Key.ToString() + "Down");

		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{

			base.OnKeyPress(e);

			engine.EventHandler.Execute("Key" + e.KeyChar + "Press");

		}

		protected override void OnKeyUp(KeyboardKeyEventArgs e)
		{

			base.OnKeyUp(e);

			engine.EventHandler.Execute("Key" + e.Key.ToString() + "Up");

		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{

			base.OnMouseDown(e);

			engine.EventHandler.Execute("MouseButton" + e.Button.ToString() + "Down");

		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{

			base.OnMouseUp(e);

			engine.EventHandler.Execute("MouseButton" + e.Button.ToString() + "Up");

		}

	}

}