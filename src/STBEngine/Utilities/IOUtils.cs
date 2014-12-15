using System;
using System.IO;
using System.Reflection;

namespace STBEngine.Utilities
{

	public static class IOUtils
	{

		public static string ReadFile(string path, bool file)
		{

			if(file)
				return ReadFile(new FileStream(path, FileMode.Open));
			else
				return ReadFile(Assembly.GetExecutingAssembly().GetManifestResourceStream(path));

		}

		public static string ReadFile(Stream stream)
		{

			return new StreamReader(stream).ReadToEnd();

		}

		public static void WriteFile(Stream stream, string data)
		{

			new StreamWriter(stream).Write(data);

		}

	}

}