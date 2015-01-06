using System;

using OpenTK;

using STBEngine.Core;
using STBEngine.Utilities;

namespace STBEngine.Rendering.Shaders
{

	public class BasicShader : Shader
	{

		private static readonly BasicShader instance = new BasicShader();

		private BasicShader()
		{

			AddVertexShader(IOUtils.LoadShader("basicVS.glsl"));
			AddGeometryShader(IOUtils.LoadShader("basicGS.glsl"));
			AddFragmentShader(IOUtils.LoadShader("lightBasicFS.glsl"));

			Compile();

		}

		public void UpdateUniforms(CoreEngine engine, Entity entity)
		{

			SetUniform("projection", engine.RenderingEngine.Camera.Projection);
			SetUniform("transformation", entity.Transformation.GetTransformation());

			SetUniform("useTexture", entity.Material.Texture.Initialized ? 1 : 0);
			SetUniform("baseColor", new Vector4(entity.Material.Color.R, entity.Material.Color.G, entity.Material.Color.B, entity.Material.Color.A));

			SetUniform("ambientLight", entity.Material.AmbientLight);

		}

		public static BasicShader Instance
		{

			get
			{

				return instance;

			}

		}

	}

}