using System;

using OpenTK.Input;

namespace STBEngine.Core
{

	public static class Input
	{

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

		public static void Update()
		{
			
			lastKeyboardState = thisKeyboardState;
			thisKeyboardState = Keyboard.GetState();

			lastMouseState = thisMouseState;
			thisMouseState = Mouse.GetState();

		}

	}

}