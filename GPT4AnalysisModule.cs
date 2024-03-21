using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure;
using Azure.AI.OpenAI;

namespace AudioTranscriptionAnalysis
{
    public class GPT4AnalysisModule
    {
        private OpenAIClient openAIClient;
        private string deploymentName;

        public GPT4AnalysisModule()
        {
            string subscriptionKey = ConfigurationModule.OpenAISubscriptionKey;
            string endpoint = ConfigurationModule.OpenAIEndpoint;
            deploymentName = ConfigurationModule.OpenAIDeploymentName;

            openAIClient = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(subscriptionKey));

            LoggingModule.LogInfo("GPT4AnalysisModule initialized.");
        }

        public async Task<string[]> AnalyzeTranscriptionsAsync(string[] transcriptions)
        {
            List<string> analysisResults = new List<string>();

            foreach (string transcription in transcriptions)
            {
                var chatCompletionsOptions = new ChatCompletionsOptions
                {
                    DeploymentName = deploymentName,
                    Messages =
                    {
                        new ChatRequestSystemMessage( "You will be given a 30 second snippet of a conversation. Give recommendations and suggestion how to improve the conversation. Offer coaching. Be concise. Use keywords only, no sentence. This is feedback for the contact center agent."),
                        new ChatRequestUserMessage( $"Here is the transcription:\n{transcription}")
                    },
                    MaxTokens = 30
                };
                var response = await openAIClient.GetChatCompletionsAsync(chatCompletionsOptions);

                if (response.GetRawResponse().Status == 200)
                {
                    var assistantMessage = response.Value.Choices[0].Message.Content;
                    analysisResults.Add(assistantMessage);

                    LoggingModule.LogInfo($"Analysis completed for transcription: {transcription}");
                }
                else
                {
                    analysisResults.Add("Error occurred during analysis.");
                    LoggingModule.LogError($"Error occurred during analysis for transcription: {transcription}");
                }
            }

            return analysisResults.ToArray();
        }
    }
}