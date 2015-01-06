using System;

using OpenTK;

using STBEngine.Core;
using STBEngine.Utilities;

namespace STBEngine.Rendering.Shaders
{

	public class GUIShader : Shader
	{

		private static readonly GUIShader instance = new GUIShader();

		private GUIShader()
		{

			AddVertexShader(IOUtils.LoadShader("guiVS.glsl"));
			AddGeometryShader(IOUtils.LoadShader("guiGS.glsl"));
			AddFragmentShader(IOUtils.LoadShader("guiFS.glsl"));

			Compile();

		}

		public void UpdateUniforms(GUIObject guiObject)
		{

			SetUniform("projection", Matrix4.CreateOrthographicOffCenter(0f, 720f, 480f, 0f, -1f, 1f));

			SetUniform("useTexture", guiObject.UseTexture ? 1 : 0);
			SetUniform("baseColor", new Vector4(guiObject.Color.R, guiObject.Color.G, guiObject.Color.B, guiObject.Color.A));

		}

		public static GUIShader Instance
		{

			get
			{

				return instance;

			}

		}

	}

}