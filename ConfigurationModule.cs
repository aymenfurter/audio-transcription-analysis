using System;
using System.Configuration;

namespace AudioTranscriptionAnalysis
{
    public static class ConfigurationModule
    {
        public static string OpenAISubscriptionKey => ConfigurationManager.AppSettings["OpenAISubscriptionKey"];
        public static string OpenAIEndpoint => ConfigurationManager.AppSettings["OpenAIEndpoint"];
        public static string OpenAIDeploymentName => ConfigurationManager.AppSettings["OpenAIDeploymentName"];
        public static string WhisperSubscriptionKey => ConfigurationManager.AppSettings["WhisperSubscriptionKey"];
        public static string WhisperEndpoint => ConfigurationManager.AppSettings["WhisperEndpoint"];
        public static string WhisperDeploymentName => ConfigurationManager.AppSettings["WhisperDeploymentName"];
        public static string AudioCaptureDevice => ConfigurationManager.AppSettings["AudioCaptureDevice"];
        public static string TranscriptionLanguage => ConfigurationManager.AppSettings["TranscriptionLanguage"];

        public static void Initialize()
        {
            // Add any necessary initialization logic here
        }
    }
}