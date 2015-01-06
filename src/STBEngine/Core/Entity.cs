using System;
using System.Collections.Generic;

using OpenTK;

using STBEngine.Core.Components;
using STBEngine.Rendering;
using STBEngine.Physics.Collision;

namespace STBEngine.Core
{

	public class Entity
	{

		private CoreEngine engine;

		private List<Component> components;

		private Transformation transformation;
		private Material material;
		private Mesh mesh;
		private List<Collider> colliders;
		private Vector3 velocity;
		private float distance;

		public Entity()
		{

			transformation = new Transformation();
			material = new Material();
			mesh = new Mesh();
			colliders = new List<Collider>();
			velocity = new Vector3(0f, 0f, 0f);
			distance = 0.5f;

			components = new List<Component>();

		}

		public void Initialize(CoreEngine engine)
		{

			this.engine = engine;

		}

		public void Update()
		{

			foreach(Component component in components)
			{

				component.Update();

			}

		}

		public void Simulate()
		{

			transformation.Translate(velocity.Normalized(), distance);

			velocity = new Vector3(0f, 0f, 0f);
			distance = 0.5f;

			foreach(Component component in components)
			{

				component.Simulate();

			}

			transformation.Translate(velocity.Normalized(), distance);

		}

		public void Terminate()
		{

			for(uint i = 0; i < components.Count; i++)
			{

				RemoveComponent(components[0]);

			}

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

		public CoreEngine Engine
		{

			get
			{

				return engine;

			}

		}

		public Transformation Transformation
		{

			get
			{

				return transformation;

			}
			set
			{

				this.transformation = value;

			}

		}

		public Material Material
		{

			get
			{

				return material;

			}
			set
			{

				this.material = value;

			}

		}

		public Mesh Mesh
		{

			get
			{

				return mesh;

			}
			set
			{

				this.mesh = value;

			}

		}

		public List<Collider> Colliders
		{

			get
			{

				return colliders;

			}
			set
			{

				this.colliders = value;

			}

		}

		public Vector3 Velocity
		{

			get
			{

				return velocity;

			}
			set
			{

				this.velocity = value;

			}

		}

		public float Distance
		{

			get
			{

				return distance;

			}
			set
			{

				this.distance = value;

			}

		}

	}

}