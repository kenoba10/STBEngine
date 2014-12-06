using System;

namespace STBEngine.Rendering
{

	public struct Index
	{

		public static readonly uint SIZE = 1;
		public static readonly uint SIZE_IN_BYTES = SIZE * 4;

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