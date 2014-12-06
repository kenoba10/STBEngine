using System;

namespace STBEngine.Core
{

	public abstract class Component
	{

		private Entity parent;

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