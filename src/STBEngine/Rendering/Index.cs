using System;

namespace STBEngine.Rendering
{

	public struct Index
	{

		private uint index;

		public Index(uint index)
		{

			this.index = index;

		}

		public uint Index_
		{

			get
			{

				return index;

			}
			set
			{

				this.index = value;

			}

		}

	}

}