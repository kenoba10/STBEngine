using System;

namespace STBEngine.Core.Components
{

	public class Attenuation
	{

		private float constant;
		private float linear;
		private float exponent;

		public Attenuation(float constant, float linear, float exponent)
		{

			this.constant = constant;
			this.linear = linear;
			this.exponent = exponent;

		}

		public float Constant
		{

			get
			{

				return constant;

			}
			set
			{

				this.constant = value;

			}

		}

		public float Linear
		{

			get
			{

				return linear;

			}
			set
			{

				this.linear = value;

			}

		}

		public float Exponent
		{

			get
			{

				return exponent;

			}
			set
			{

				this.exponent = value;

			}

		}

	}

}