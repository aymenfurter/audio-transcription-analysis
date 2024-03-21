<img src="https://github.com/aymenfurter/audio-transcription-analysis/blob/main/screenshot.png?raw=true">

# Audio Transcription and Analysis

This application captures audio from the user's computer and microphone, transcribes the audio using the Azure OpenAI Whisper model, and performs sentiment analysis, real-time suggestions, emotional intelligence coaching, and conflict resolution using the Azure OpenAI GPT-4 model.

## Prerequisites

Before running the application, ensure that you have the following:

- An Azure subscription
- Access granted to Azure OpenAI Service in the desired Azure subscription
- An Azure OpenAI resource with the GPT-4 and Whisper models deployed
- .NET Framework 4.7.2 or later installed on your machine
- Visual Studio or another compatible IDE

## Setup

1. Clone the repository or download the source code.

2. Open the solution file (`AudioTranscriptionAnalysis.sln`) in Visual Studio.

3. Restore the NuGet packages by right-clicking on the solution in the Solution Explorer and selecting "Restore NuGet Packages".

4. Open the `App.config` file and replace the placeholder values with your actual values:
    <add key="OpenAISubscriptionKey" value="replaceme" />
    <add key="OpenAIEndpoint" value="https://replaceme.openai.azure.com/" />
    <add key="OpenAIDeploymentName" value="gpt-35-turbo" />
    <add key="WhisperSubscriptionKey" value="replaceme" />
    <add key="WhisperEndpoint" value="https://replaceme.openai.azure.com/" />
    <add key="WhisperDeploymentName" value="whisper" />
    <add key="AudioCaptureDevice" value="tbd" />
    <add key="TranscriptionLanguage" value="en-US" />

   Note: Make sure to encrypt your sensitive information before storing it in the configuration file.

5. Build the solution by selecting "Build" > "Build Solution" from the menu or pressing Ctrl+Shift+B.

## Running the Application

1. Run the application by selecting "Debug" > "Start Debugging" from the menu or pressing F5.

2. The application window will appear, displaying two text boxes and two buttons.

3. Click the "Start" button to begin the audio capture and analysis process.

4. The application will continuously capture audio from your computer and microphone in 10-second intervals, transcribe the audio using the Whisper model, and perform analysis using the GPT-4 model.

5. The transcriptions and analysis results will be displayed in the respective text boxes in real-time.

6. To stop the audio capture and analysis process, click the "Stop" button.

7. You can close the application window when you're done.

## Logging and Error Handling

The application logs information, warnings, errors, and exceptions to both the console and a log file named `app.log` in the `logs` directory where the application is running.

If any errors occur during the execution of the application, error messages will be displayed in the user interface, and detailed error information will be logged to the log file.

## Dependencies

The application relies on the following NuGet packages:

- Azure.AI.OpenAI
- Microsoft.CognitiveServices.Speech
- NAudio
- Newtonsoft.Json
- System.Configuration.ConfigurationManager
- System.Windows.Forms

These packages are automatically restored when you build the solution.
