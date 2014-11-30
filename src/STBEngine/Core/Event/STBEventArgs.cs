using System;

namespace STBEngine.Core.Event
{

	public class STBEventArgs : EventArgs
	{

		private string _event;

		public STBEventArgs(string _event) : base()
		{

			this._event = _event;

		}

		public string Event
		{

			get
			{

				return _event;

			}

		}

	}

}