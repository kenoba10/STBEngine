using System;

namespace STBEngine.Core
{

	public interface IGame
	{

		void Initialize();

		void Update();

		void Terminate();

		string Title
		{

			get;

		}

	}

}