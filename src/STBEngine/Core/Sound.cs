using System;
using System.IO;

using OpenTK;
using OpenTK.Audio.OpenAL;

using STBEngine.Utilities;

namespace STBEngine.Core
{

	public class Sound
	{

		private int sound;

		public Sound()
		{

			sound = 0;

		}

		public void LoadSound(Stream stream)
		{

			int channels;
			int bitsPerSample;
			int sampleRate;

			byte[] data = IOUtils.LoadSound(stream, out channels, out bitsPerSample, out sampleRate);

			ALFormat format = channels == 1 && bitsPerSample == 8 ? ALFormat.Mono8 : channels == 1 && bitsPerSample == 16 ? ALFormat.Mono16 : channels == 2 && bitsPerSample == 8 ? ALFormat.Stereo8 : channels == 2 && bitsPerSample == 16 ? ALFormat.Stereo16 : (ALFormat) 0;

			int buffer = AL.GenBuffer();

			AL.BufferData(buffer, format, data, data.Length, sampleRate);

			sound = AL.GenSource();

			AL.Source(sound, ALSourcei.Buffer, buffer);

			AL.DeleteBuffer(buffer);

		}

		public void Play()
		{

			AL.SourcePlay(sound);

		}

		public void Pause()
		{

			AL.SourcePause(sound);

		}

		public void Stop()
		{

			AL.SourceStop(sound);

		}

		public void UnloadSound()
		{

			AL.DeleteSource(sound);

		}

		public Vector3 Position
		{

			set
			{

				AL.Source(sound, ALSource3f.Position, ref value);

			}

		}

	}

}