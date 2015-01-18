using System;

using STBEngine.Rendering;
using STBEngine.Rendering.Models;

namespace STBEngine.Core.Components
{

	public class MeshRenderer : Component
	{

		private Mesh mesh;

		public MeshRenderer(Model model)
		{

			mesh = new Mesh(model);

		}

		public override void Render()
		{
			
			mesh.Draw();

		}

		public override void Terminate()
		{

			mesh.Delete();

		}

	}

}