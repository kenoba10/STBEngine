using System;

namespace STBEngine.Core.Event
{

	public class STBEventArgs : EventArgs
	{

		private string _event;
		private string info;

		public STBEventArgs(string _event) : this(_event, "")
		{

		}

		public STBEventArgs(string _event, string info) : base()
		{

			this._event = _event;
			this.info = info;

		}

		public string Event
		{

			get
			{

				return _event;

			}

		}

		public string Info
		{

			get
			{

				return info;

			}

		}

	}

}