using System;

namespace STBEngine.Core.Components
{

	public abstract class Component
	{

		protected Entity parent;

		public virtual void Initialize()
		{

		}

		public virtual void Update()
		{

		}

		public virtual void Terminate()
		{

		}

		public Entity Parent
		{

			get
			{

				return parent;

			}
			set
			{

				this.parent = value;

			}

		}

	}

}