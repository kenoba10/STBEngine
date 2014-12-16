using System;
using System.IO;

using OpenTK;
using OpenTK.Audio.OpenAL;

using STBEngine.Utilities;

namespace STBEngine.Core
{
	public class Sound
	{

		private int source;

		public Sound()
		{

			source = 0;

		}

		public void LoadAudio(Stream stream)
		{

			int channels;
			int bitsPerSample;
			int sampleRate;

			byte[] data = IOUtils.LoadSound(stream, out channels, out bitsPerSample, out sampleRate);

			ALFormat format = channels == 1 && bitsPerSample == 8 ? ALFormat.Mono8 : channels == 1 && bitsPerSample == 16 ? ALFormat.Mono16 : channels == 2 && bitsPerSample == 8 ? ALFormat.Stereo8 : channels == 2 && bitsPerSample == 16 ? ALFormat.Stereo16 : (ALFormat) 0;

			int buffer = AL.GenBuffer();

			AL.BufferData(buffer, format, data, data.Length, sampleRate);

			source = AL.GenSource();

			AL.Source(source, ALSourcei.Buffer, buffer);

			AL.DeleteBuffer(buffer);

		}

		public void Play()
		{

			AL.SourcePlay(source);

		}

		public void Pause()
		{

			AL.SourcePause(source);

		}

		public void Stop()
		{

			AL.SourceStop(source);

		}

		public void UnloadAudio()
		{

			AL.DeleteSource(source);

		}

		public Vector3 Position
		{

			set
			{

				AL.Source(source, ALSource3f.Position, ref value);

			}

		}

	}

}