using System;

namespace STBEngine.Core
{

	public interface IGame
	{

		void Initialize(CoreEngine engine);

		void Update();

		void Terminate();

		string Title
		{

			get;

		}

	}

}