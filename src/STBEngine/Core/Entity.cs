using System;
using System.Collections.Generic;

using STBEngine.Rendering;

namespace STBEngine.Core
{

	public class Entity
	{

		private List<Component> components;

		private Mesh mesh;
		private Shader shader;

		public Entity()
		{

			mesh = new Mesh();
			shader = new Shader();

			components = new List<Component>();

		}

		public void Initialize()
		{

		}

		public void Update()
		{

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