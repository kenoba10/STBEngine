using System;

using OpenTK;
using OpenTK.Input;

namespace STBEngine.Core
{

	public static class Input
	{

		private static Window window;

		private static KeyboardState lastKeyboardState;
		private static KeyboardState thisKeyboardState;

		private static MouseState lastMouseState;
		private static MouseState thisMouseState;

		public static bool GetKey(Key key)
		{

			return thisKeyboardState.IsKeyDown(key);

		}

		public static bool GetKeyDown(Key key)
		{

			return thisKeyboardState.IsKeyDown(key) && lastKeyboardState.IsKeyUp(key);

		}

		public static bool GetKeyUp(Key key)
		{

			return thisKeyboardState.IsKeyUp(key) && lastKeyboardState.IsKeyDown(key);

		}

		public static bool GetMouseButton(MouseButton button)
		{

			return thisMouseState.IsButtonDown(button);

		}

		public static bool GetMouseButtonDown(MouseButton button)
		{

			return thisMouseState.IsButtonDown(button) && lastMouseState.IsButtonUp(button);

		}

		public static bool GetMouseButtonUp(MouseButton button)
		{

			return thisMouseState.IsButtonUp(button) && lastMouseState.IsButtonDown(button);

		}

		public static Vector2 GetMousePosition()
		{
			return new Vector2(thisMouseState.X - lastMouseState.X, thisMouseState.Y - lastMouseState.Y);

		}

		public static void SetMousePosition()
		{

			Mouse.SetPosition(window.X + 256, window.Y + 256);

		}

		public static void LockMouse(bool lockMouse)
		{

			window.CursorVisible = !lockMouse;

		}

		public static void Update(Window window)
		{

			Input.window = window;
			
			lastKeyboardState = thisKeyboardState;
			thisKeyboardState = Keyboard.GetState();

			lastMouseState = thisMouseState;
			thisMouseState = Mouse.GetState();

		}

	}

}