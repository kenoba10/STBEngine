using System;

using OpenTK;

using STBEngine.Core;
using STBEngine.Rendering;
using STBEngine.Rendering.Models;

namespace STBEngine.Physics.Collision.Colliders
{

	//WARNING: Bounding Spheres and Polygons Are VERY BUGGY.  Use At Own Risk!
	public class BoundingPolygon : Collider
	{

		private Model model;

		private Model modelTransformed;

		public BoundingPolygon(Model model)
		{

			this.model = model;

			modelTransformed = new Model();

		}

		public override Intersection Intersect(Collider collider)
		{

			if(collider.Type == ColliderType.BoundingPolygon)
			{

				return Detection.Intersect(this, (BoundingPolygon) collider);

			}
			else if(collider.Type == ColliderType.BoundingSphere)
			{

				return Detection.Intersect((BoundingSphere) collider, this);

			}

			return new Intersection(false, new Vector3(0f, 0f, 0f));

		}

		public override void Transform(Transformation transformation)
		{

			modelTransformed.Vertices.Clear();

			for(int i = 0; i < model.VertexCount; i++)
			{

				Vertex vertex = model.Vertices[i];

				vertex.Position += transformation.Position;
				vertex.Position *= transformation.Scale;

				modelTransformed.AddVertex(vertex);

			}

		}

		public Model Model
		{

			get
			{

				return modelTransformed;

			}
			set
			{

				this.model = value;

			}

		}

		public override ColliderType Type
		{

			get
			{

				return ColliderType.BoundingPolygon;

			}

		}

	}

}