using System;

using OpenTK;

namespace STBEngine.Core.Components
{

	public class SpotLight : Component
	{

		private PointLight base_;
		private Vector3 direction;
		private float cutoff;

		public SpotLight(PointLight base_, Vector3 direction, float cutoff)
		{

			this.base_ = base_;
			this.direction = direction;
			this.cutoff = cutoff;

		}

		public PointLight Base
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

		public float CutOff
		{

			get
			{

				return cutoff;

			}
			set
			{

				this.cutoff = value;

			}

		}

	}

}