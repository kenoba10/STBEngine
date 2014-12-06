using System;
using System.IO;

namespace STBEngine.Utilities
{

	public static class IOUtils
	{

		public static string ReadFile(string path)
		{

			return File.ReadAllText(path);

		}

		public static void WriteFile(string path, string data)
		{

			File.WriteAllText(path, data);

		}

	}

}