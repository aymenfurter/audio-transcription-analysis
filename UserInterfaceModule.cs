using System;
using System.Windows.Forms;

namespace AudioTranscriptionAnalysis
{
    public class UserInterfaceModule
    {
        private Form mainForm;
        private TextBox transcriptionTextBox;
        private TextBox analysisResultsTextBox;
        private Button startButton;
        private Button stopButton;
        private AudioTranscriptionAnalysis audioTranscriptionAnalysis;

        public UserInterfaceModule()
        {
            InitializeUI();
            audioTranscriptionAnalysis = new AudioTranscriptionAnalysis(this);
        }

        private void InitializeUI()
        {
            mainForm = new Form();
            mainForm.Text = "Audio Transcription Analysis";
            mainForm.FormBorderStyle = FormBorderStyle.FixedSingle;
            mainForm.MaximizeBox = false;
            mainForm.StartPosition = FormStartPosition.CenterScreen;
            mainForm.ClientSize = new System.Drawing.Size(800, 600);

            transcriptionTextBox = new TextBox();
            transcriptionTextBox.Multiline = true;
            transcriptionTextBox.ScrollBars = ScrollBars.Vertical;
            transcriptionTextBox.ReadOnly = true;
            transcriptionTextBox.Location = new System.Drawing.Point(20, 20);
            transcriptionTextBox.Size = new System.Drawing.Size(760, 200);
            mainForm.Controls.Add(transcriptionTextBox);

            analysisResultsTextBox = new TextBox();
            analysisResultsTextBox.Multiline = true;
            analysisResultsTextBox.ScrollBars = ScrollBars.Vertical;
            analysisResultsTextBox.ReadOnly = true;
            analysisResultsTextBox.Location = new System.Drawing.Point(20, 240);
            analysisResultsTextBox.Size = new System.Drawing.Size(760, 200);
            mainForm.Controls.Add(analysisResultsTextBox);

            startButton = new Button();
            startButton.Text = "Start";
            startButton.Location = new System.Drawing.Point(20, 460);
            startButton.Click += StartButton_Click;
            mainForm.Controls.Add(startButton);

            stopButton = new Button();
            stopButton.Text = "Stop";
            stopButton.Location = new System.Drawing.Point(120, 460);
            stopButton.Click += StopButton_Click;
            stopButton.Enabled = false;
            mainForm.Controls.Add(stopButton);
        }

        public void ShowUI()
        {
            Application.Run(mainForm);
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            stopButton.Enabled = true;
            await audioTranscriptionAnalysis.StartAnalysis();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            stopButton.Enabled = false;
            audioTranscriptionAnalysis.StopAnalysis();
        }

        public void DisplayTranscription(string transcription)
        {
            transcriptionTextBox.AppendText(transcription + Environment.NewLine);
        }

        public void DisplayAnalysisResults(string analysisResults)
        {
            analysisResultsTextBox.AppendText(analysisResults + Environment.NewLine);
        }

        public void DisplayErrorMessage(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}