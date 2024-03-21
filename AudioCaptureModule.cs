using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.IO;
using System.Threading.Tasks;

namespace AudioTranscriptionAnalysis
{
    public class AudioCaptureModule
    {
        private readonly MMDevice audioDevice;
        private WasapiCapture audioCapture; // Using WasapiCapture for input
        private MemoryStream audioStream;

        public AudioCaptureModule()
        {
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            audioDevice = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
            foreach (var endPoint in deviceEnumerator.EnumerateAudioEndPoints(DataFlow.Capture, DeviceState.Active))
            {
                Console.WriteLine($"{endPoint.FriendlyName}");
                if (endPoint.FriendlyName.Contains("G733"))
                {
                    audioDevice = endPoint;
                    break;
                }
            }

            LoggingModule.LogInfo("AudioCaptureModule initialized.");
        }

        public async Task<byte[]> CaptureAudioAsync(int durationMilliseconds)
        {
            audioCapture = new WasapiCapture(audioDevice); // Using WasapiCapture for input
            audioStream = new MemoryStream();
            var waveFormat = audioCapture.WaveFormat; // Capture the WaveFormat of the recording device

            // Attach an event handler to capture data
            audioCapture.DataAvailable += (s, a) =>
            {
                audioStream.Write(a.Buffer, 0, a.BytesRecorded);
            };

            audioCapture.StartRecording();
            LoggingModule.LogInfo("Audio capture started.");

            await Task.Delay(durationMilliseconds);

            audioCapture.StopRecording();
            LoggingModule.LogInfo("Audio capture stopped.");

            audioCapture.Dispose();

            // Now let's prepend the WAV header information
            var wavStream = new MemoryStream();
            WaveFileWriter.WriteWavFileToStream(wavStream, new RawSourceWaveStream(audioStream.ToArray(), 0, (int)audioStream.Length, waveFormat));

            byte[] wavData = wavStream.ToArray();

            // Clean up
            audioStream.Dispose();
            wavStream.Dispose();

            return wavData;
        }
    }
}
