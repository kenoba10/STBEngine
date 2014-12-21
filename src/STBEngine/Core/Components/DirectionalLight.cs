using System;

using OpenTK;

namespace STBEngine.Core.Components
{

	public class DirectionalLight : Component
	{

		private BaseLight base_;
		private Vector3 direction;

		public DirectionalLight(BaseLight base_, Vector3 direction)
		{

			this.base_ = base_;
			this.direction = direction;

		}

		public BaseLight Base
		{

			get
			{

				return base_;

			}
			set
			{

				this.base_ = value;

			}

		}

		public Vector3 Direction
		{

			get
			{

				return direction;

			}
			set
			{

				this.direction = value;

			}

		}

	}

}