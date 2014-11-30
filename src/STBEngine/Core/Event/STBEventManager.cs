using System;

namespace STBEngine.Core.Event
{

	public delegate void STBEventHandler(STBEventArgs e);

	public class STBEventManager
	{

		private event STBEventHandler Event;

		public STBEventManager()
		{

			Subscribe(OnEvent);

		}

		public void Subscribe(STBEventHandler handler)
		{

			Event += handler;

		}

		public void Unsubscribe(STBEventHandler handler)
		{

			Event -= handler;

		}

		public void Execute(string _event)
		{

			Event.Invoke(new STBEventArgs(_event));

		}

		private void OnEvent(STBEventArgs e)
		{

		}

	}

}