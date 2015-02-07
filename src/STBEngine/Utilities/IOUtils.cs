using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;

namespace STBEngine.Utilities
{

	public static class IOUtils
	{

		public static Stream GetAssemblyStream(object obj, string path)
		{

			return Assembly.GetAssembly(obj.GetType()).GetManifestResourceStream(path);

		}

		public static string ReadFile(Stream stream)
		{

			return new StreamReader(stream).ReadToEnd();

		}

		public static void WriteFile(Stream stream, string data)
		{

			new StreamWriter(stream).Write(data);

		}

		public static string LoadShader(string name)
		{

			string shader = "";

			using(StreamReader reader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("STBEngine.res.shaders." + name)))
			{

				string line;

				while((line = reader.ReadLine()) != null)
				{

					if(line.StartsWith("#include"))
					{

						shader += IOUtils.LoadShader(line.Substring(10, line.Length - 11)) + '\n';

					}
					else
					{

						shader += line + '\n';

					}

				}

			}

			return shader;

		}

		public static IntPtr LoadTexture(Stream stream, out int width, out int height)
		{

			Bitmap bitmap = new Bitmap(stream);

			width = bitmap.Width;
			height = bitmap.Height;

			return bitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb).Scan0;

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