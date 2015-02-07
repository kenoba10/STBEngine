using System;
using System.IO;
using System.Reflection;

using STBEngine.Core;

namespace STBEngine
{

	public class Start
	{

		public static void Main(string[] args)
		{

			CoreEngine engine = new CoreEngine();

			Assembly dll = Assembly.LoadFile(args[0]);

			foreach(Type type in dll.GetExportedTypes())
			{

				object game = Activator.CreateInstance(type);

				if(game is IGame)
				{

					engine.Run((IGame) game, 20, 60);

					break;

				}

			}

		}

	}

}