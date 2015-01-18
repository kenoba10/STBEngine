using System;

using OpenTK;

using STBEngine.Core;
using STBEngine.Core.Components;
using STBEngine.Utilities;

namespace STBEngine.Rendering.Shaders
{

	public class SpotLightShader : Shader
	{

		private static readonly SpotLightShader instance = new SpotLightShader();

		private SpotLightShader()
		{

			AddVertexShader(IOUtils.LoadShader("basicVS.glsl"));
			AddGeometryShader(IOUtils.LoadShader("basicGS.glsl"));
			AddFragmentShader(IOUtils.LoadShader("lightSpotFS.glsl"));

			Compile();

		}

		public void UpdateUniforms(CoreEngine engine, Entity entity, SpotLight light)
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

			SetUniform("light.base.base.color", new Vector4(light.Base.Base.Color.R, light.Base.Base.Color.G, light.Base.Base.Color.B, light.Base.Base.Color.A));
			SetUniform("light.base.base.intensity", light.Base.Base.Intensity);
			SetUniform("light.base.attenuation.constant", light.Base.Attenuation.Constant);
			SetUniform("light.base.attenuation.linear", light.Base.Attenuation.Linear);
			SetUniform("light.base.attenuation.exponent", light.Base.Attenuation.Exponent);
			SetUniform("light.base.position", light.Base.Position);
			SetUniform("light.base.range", light.Base.Range);
			SetUniform("light.direction", light.Direction);
			SetUniform("light.cutoff", light.CutOff);

		}

		public static SpotLightShader Instance
		{

			get
			{

				return instance;

			}

		}

	}

}