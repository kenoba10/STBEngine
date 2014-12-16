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

		public static byte[] LoadSound(Stream stream, out int channels, out int bits, out int rate)
		{

			using(MemoryStream newStream = new MemoryStream())
			{

				stream.CopyTo(newStream);

				byte[] bytes = newStream.ToArray();

				channels = (bytes[22]) | (bytes[23] << 8);
				bits = (bytes[34]) | (bytes[35] << 8);
				rate = (bytes[24]) | (bytes[25] << 8) | (bytes[26] << 16) | (bytes[27] << 32);

				byte[] data = new byte[bytes.Length - 44];

				for(uint i = 0; i < bytes.Length - 44; i++)
				{

					data[i] = bytes[i + 44];

				}

				return data;

			}

		}

	}

}