using System;
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

				if(typeof(IGame).IsAssignableFrom(type))
				{

					engine.Run((IGame) Activator.CreateInstance(type, engine), args.Length > 1 ? uint.Parse(args[1]) : 20, args.Length > 2 ? uint.Parse(args[2]) : 60);

					break;

				}

			}

		}

	}

}