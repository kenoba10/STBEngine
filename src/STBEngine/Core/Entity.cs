using System;
using System.Collections.Generic;

using OpenTK;

using STBEngine.Core.Components;
using STBEngine.Rendering;

namespace STBEngine.Core
{

	public class Entity
	{

		private List<Component> components;

		private Transformation transformation;
		private Material material;
		private Mesh mesh;
		private Shader shader;

		public Entity()
		{

			transformation = new Transformation();
			material = new Material();
			mesh = new Mesh();
			shader = new Shader();

			components = new List<Component>();

		}

		public void Initialize()
		{

		}

		public void Update()
		{

			shader.Bind();

			shader.SetUniform("projection", CoreEngine.Instance.RenderingEngine.Camera.Projection);
			shader.SetUniform("transformation", transformation.GetTransformation());

			shader.SetUniform("useTexture", material.Texture.Initialized ? 1 : 0);
			shader.SetUniform("baseColor", new Vector4(material.Color.R, material.Color.G, material.Color.B, material.Color.A));

			shader.UnBind();

			foreach(Component component in components)
			{

				component.Update();

			}

		}

		public void Terminate()
		{

			for(uint i = 0; i < components.Count; i++)
			{

				RemoveComponent(components[0]);

			}

			shader.Delete();
			mesh.RemoveVertices();
			material.Terminate();

		}

		public void AddComponent(Component component)
		{

			component.Parent = this;

			component.Initialize();

			components.Add(component);

		}

		public void RemoveComponent(Component component)
		{

			components.Remove(component);

			component.Terminate();

		}

		public Transformation Transformation
		{

			get
			{

				return transformation;

			}

		}

		public Material Material
		{

			get
			{

				return material;

			}

		}

		public Mesh Mesh
		{

			get
			{

				return mesh;

			}

		}

		public Shader Shader
		{

			get
			{

				return shader;

			}

		}

	}

}