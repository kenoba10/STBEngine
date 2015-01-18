using System;

using OpenTK;

using STBEngine.Core;
using STBEngine.Core.Components;
using STBEngine.Utilities;

namespace STBEngine.Rendering.Shaders
{

	public class DirectionalLightShader : Shader
	{

		private static readonly DirectionalLightShader instance = new DirectionalLightShader();

		private DirectionalLightShader()
		{

			AddVertexShader(IOUtils.LoadShader("basicVS.glsl"));
			AddGeometryShader(IOUtils.LoadShader("basicGS.glsl"));
			AddFragmentShader(IOUtils.LoadShader("lightDirectionalFS.glsl"));

			Compile();

		}

		public void UpdateUniforms(CoreEngine engine, Entity entity, DirectionalLight light)
		{

			SetUniform("projection", engine.RenderingEngine.Camera.Projection);
			SetUniform("transformation", entity.Transformation.GetTransformation());

			SetUniform("eyePosition", engine.RenderingEngine.Camera.Parent.Transformation.Position);

			SetUniform("specularIntensity", entity.Material.SpecularIntensity);
			SetUniform("specularExponent", entity.Material.SpecularExponent);

			SetUniform("displacementScale", entity.Material.DisplacementScale);
			SetUniform("displacementBias", -(entity.Material.DisplacementScale / 2f) + (entity.Material.DisplacementScale / 2f) * entity.Material.DisplacementOffset);

			SetUniform("useDisplacementMap", entity.Material.DisplacementMap.Initialized ? 1 : 0);
			SetUniform("displacementMap", 0);

			SetUniform("useNormalMap", entity.Material.NormalMap.Initialized ? 1 : 0);
			SetUniform("normalMap", 1);

			SetUniform("light.base.color", new Vector4(light.Base.Color.R, light.Base.Color.G, light.Base.Color.B, light.Base.Color.A));
			SetUniform("light.base.intensity", light.Base.Intensity);
			SetUniform("light.direction", light.Direction);

		}

		public static DirectionalLightShader Instance
		{

			get
			{

				return instance;

			}

		}

	}

}