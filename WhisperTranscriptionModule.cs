using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AudioTranscriptionAnalysis
{
    public class WhisperTranscriptionModule
    {
        private HttpClient httpClient;
        private string subscriptionKey;
        private string endpoint;
        private string deploymentName;

        public WhisperTranscriptionModule()
        {
            subscriptionKey = ConfigurationModule.WhisperSubscriptionKey;
            endpoint = ConfigurationModule.WhisperEndpoint;
            deploymentName = ConfigurationModule.WhisperDeploymentName;

            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("api-key", subscriptionKey);

            LoggingModule.LogInfo("WhisperTranscriptionModule initialized.");
        }

    

        public async Task<string[]> TranscribeAudioSegmentsAsync(byte[] audioSegment)
        {
            List<string> transcriptions = new List<string>();
            int channels = 1;

                byte[] audioWithHeader = audioSegment;
                SaveWavFileForDebugging(audioWithHeader);

                using (var content = new MultipartFormDataContent())
                {
                    var byteContent = new ByteArrayContent(audioWithHeader);
                    byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("audio/wav");
                    content.Add(byteContent, "file", "audio.wav");

                    var response = await httpClient.PostAsync($"{endpoint}/openai/deployments/{deploymentName}/audio/transcriptions?api-version=2024-02-01", content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JObject.Parse(responseContent);
                        var transcription = result["text"].ToString();
                        transcriptions.Add(transcription);
                        LoggingModule.LogInfo("Transcription completed for audio segment.");
                    }
                    else
                    {
                        LoggingModule.LogError($"Error: {responseContent}");
                        transcriptions.Add(string.Empty);
                    }
                }

            // print transcriptions
            foreach (var transcription in transcriptions)
            {
                Console.WriteLine(transcription);
            }

            return transcriptions.ToArray();
        }
        public void SaveWavFileForDebugging(byte[] audioWithHeader)
        {
            try
            {
                // Adjust the path and filename as necessary
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DebugAudio", $"{Guid.NewGuid()}.wav");
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Ensure the directory exists
                File.WriteAllBytes(filePath, audioWithHeader);
                LoggingModule.LogInfo($"WAV file saved for debugging: {filePath}");
            }
            catch (Exception ex)
            {
                LoggingModule.LogError($"Failed to save WAV file for debugging: {ex.Message}");
            }
        }
    }
}