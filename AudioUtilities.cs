using System;
using System.IO;
using NAudio.Wave;

namespace AudioTranscriptionAnalysis
{
    public static class AudioUtilities
    {
        public static byte[] ConvertWavToBytes(string wavFilePath)
        {
            using (var audioFile = new AudioFileReader(wavFilePath))
            {
                int bytesPerSample = audioFile.WaveFormat.BitsPerSample / 8;
                int numSamples = (int)(audioFile.Length / bytesPerSample);
                byte[] audioBytes = new byte[numSamples * bytesPerSample];

                int bytesRead = audioFile.Read(audioBytes, 0, audioBytes.Length);
                if (bytesRead != audioBytes.Length)
                {
                    throw new Exception("Unexpected number of bytes read from WAV file.");
                }

                return audioBytes;
            }
        }

        public static void SaveBytesToWav(byte[] audioBytes, string wavFilePath, int sampleRate, int bitsPerSample, int channels)
        {
            WaveFormat waveFormat = new WaveFormat(sampleRate, bitsPerSample, channels);

            using (var memoryStream = new MemoryStream(audioBytes))
            using (var waveFileWriter = new WaveFileWriter(wavFilePath, waveFormat))
            {
                memoryStream.CopyTo(waveFileWriter);
            }
        }


    }
}