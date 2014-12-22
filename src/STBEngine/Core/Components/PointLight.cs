using System;

using OpenTK;

namespace STBEngine.Core.Components
{

	public class PointLight : Component
	{

		private BaseLight base_;
		private Attenuation attenuation;
		private Vector3 position;
		private float range;

		public PointLight(BaseLight base_, Attenuation attenuation, Vector3 position, float range)
		{

			this.base_ = base_;
			this.attenuation = attenuation;
			this.position = position;
			this.range = range;

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

		public Attenuation Attenuation
		{

			get
			{

				return attenuation;

			}
			set
			{

				this.attenuation = value;

			}

		}

		public Vector3 Position
		{

			get
			{

				return position;

			}
			set
			{

				this.position = value;

			}

		}

		public float Range
		{

			get
			{

				return range;

			}
			set
			{

				this.range = value;

			}

		}

	}

}