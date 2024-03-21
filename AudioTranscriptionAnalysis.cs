using System;
using System.Threading.Tasks;

namespace AudioTranscriptionAnalysis
{
    public class AudioTranscriptionAnalysis
    {
        private AudioCaptureModule audioCaptureModule;
        private AudioSegmentationModule audioSegmentationModule;
        private WhisperTranscriptionModule whisperTranscriptionModule;
        private GPT4AnalysisModule gpt4AnalysisModule;
        private UserInterfaceModule userInterfaceModule;
        private bool isAnalysisRunning;

        public AudioTranscriptionAnalysis(UserInterfaceModule userInterfaceModule)
        {
            this.userInterfaceModule = userInterfaceModule;
            audioCaptureModule = new AudioCaptureModule();
            audioSegmentationModule = new AudioSegmentationModule();
            whisperTranscriptionModule = new WhisperTranscriptionModule();
            gpt4AnalysisModule = new GPT4AnalysisModule();

            LoggingModule.LogInfo("AudioTranscriptionAnalysis initialized.");
        }

        public async Task StartAnalysis()
        {
            isAnalysisRunning = true;
            LoggingModule.LogInfo("Analysis started.");

            try
            {
                while (isAnalysisRunning)
                {
                    // Capture 30 seconds of audio
                    byte[] audioData = await audioCaptureModule.CaptureAudioAsync(10000); // 30 seconds

                    // Save for debugging
                    //WhisperTranscriptionModule.SaveWavFileForDebugging(audioWithHeader);

                    // Transcribe audio segment using Whisper
                    string[] transcriptions = await whisperTranscriptionModule.TranscribeAudioSegmentsAsync(audioData);

                    // Display the transcriptions
                    userInterfaceModule.DisplayTranscription(string.Join(Environment.NewLine, transcriptions));

                     // Perform analysis and generate suggestions using GPT-4
                    string[] analysisResults = await gpt4AnalysisModule.AnalyzeTranscriptionsAsync(transcriptions);

                    // Display the analysis results in the user interface
                    userInterfaceModule.DisplayAnalysisResults(string.Join(Environment.NewLine, analysisResults));
                }
            }
            catch (Exception ex)
            {
                LoggingModule.LogException(ex);
                userInterfaceModule.DisplayErrorMessage("An error occurred. Please check the logs for more information.");
                isAnalysisRunning = false;
            }

            LoggingModule.LogInfo("Analysis stopped.");
        }
        

        public void StopAnalysis()
        {
            isAnalysisRunning = false;
        }
    }
}