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

		public Window(string title, int antiAliasing, CoreEngine engine) : base(1080, 720, new GraphicsMode(32, 24, 0, antiAliasing), title, GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.Default)
		{

			this.engine = engine;

			AC = new AudioContext();

			engine.Window = this;

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

			engine.EventHandler.Execute("KeyDown", e.Key.ToString());

		}

		protected override void OnKeyPress(KeyPressEventArgs e)
		{

			base.OnKeyPress(e);

			engine.EventHandler.Execute("KeyPress", e.KeyChar.ToString());

		}

		protected override void OnKeyUp(KeyboardKeyEventArgs e)
		{

			base.OnKeyUp(e);

			engine.EventHandler.Execute("KeyUp", e.Key.ToString());

		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{

			base.OnMouseDown(e);

			engine.EventHandler.Execute("MouseButtonDown", e.Button.ToString() + "/" + e.X + "/" + e.Y);

		}

		protected override void OnMouseUp(MouseButtonEventArgs e)
		{

			base.OnMouseUp(e);

			engine.EventHandler.Execute("MouseButtonUp", e.Button.ToString() + "/" + e.X + "/" + e.Y);

		}

		protected override void OnMouseMove(MouseMoveEventArgs e)
		{

			base.OnMouseMove(e);

			engine.EventHandler.Execute("MouseMove", e.X + "/" + e.Y + "/" + e.XDelta + "/" + e.YDelta);

		}

		public int AntiAliasing
		{

			get
			{

				return Context.GraphicsMode.Samples;

			}

		}

		public WindowState State
		{

			get
			{

				return WindowState;

			}
			set
			{

				this.WindowState = value;

			}

		}

		public VSyncMode VerticalSync
		{

			get
			{

				return VSync;

			}
			set
			{

				this.VSync = value;

			}

		}

	}

}