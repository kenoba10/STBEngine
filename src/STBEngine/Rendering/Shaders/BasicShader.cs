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

			SetUniform("eyePosition", engine.RenderingEngine.Camera.Parent.Transformation.Position);

			SetUniform("displacementScale", entity.Material.DisplacementScale);
			SetUniform("displacementBias", -(entity.Material.DisplacementScale / 2f) + (entity.Material.DisplacementScale / 2f) * entity.Material.DisplacementOffset);

			SetUniform("useDisplacementMap", entity.Material.DisplacementMap.Initialized ? 1 : 0);
			SetUniform("displacementMap", 0);

			SetUniform("useTexture", entity.Material.Texture.Initialized ? 1 : 0);

			SetUniform("baseColor", new Vector4(entity.Material.Color.R, entity.Material.Color.G, entity.Material.Color.B, entity.Material.Color.A));
			SetUniform("activeTexture", 1);

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