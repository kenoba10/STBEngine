using System;
using System.Collections.Generic;

using OpenTK;

using STBEngine.Core.Components;
using STBEngine.Physics.Collision.Colliders;

namespace STBEngine.Core
{

	public class Entity
	{

		private CoreEngine engine;

		private Transformation transformation;
		private Material material;
		private AxisAlignedBoundingBox aabb;
		private List<Collider> colliders;

		private Vector3 velocity;
		private float mass;

		private List<Component> components;

		public Entity()
		{

			transformation = new Transformation();
			material = new Material();
			aabb = new AxisAlignedBoundingBox(new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f));
			colliders = new List<Collider>();

			velocity = new Vector3(0f, 0f, 0f);
			mass = 1f;

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

			foreach(Component component in components)
			{

				component.Simulate();

			}

		}

		public void Render()
		{

			foreach(Component component in components)
			{

				component.Render();

			}

		}

		public void Terminate()
		{

			for(uint i = 0; i < components.Count; i++)
			{

				RemoveComponent(components[0]);

			}

			material.Terminate();

		}

		public void ApplyForce(Vector3 force)
		{

			velocity -= (force / mass);

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

		public AxisAlignedBoundingBox AABB
		{

			get
			{

				return aabb;

			}
			set
			{

				this.aabb = value;

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

		public float Mass
		{

			get
			{

				return mass;

			}
			set
			{

				this.mass = value;

			}

		}

	}

}