using System;
using System.Windows.Forms;

namespace AudioTranscriptionAnalysis
{
    public static class ErrorHandlingModule
    {
        public static void HandleException(Exception exception)
        {
            LoggingModule.LogException(exception);
            DisplayErrorMessage(exception.Message);
        }

        public static void HandleError(string errorMessage)
        {
            LoggingModule.LogError(errorMessage);
            DisplayErrorMessage(errorMessage);
        }

        private static void DisplayErrorMessage(string errorMessage)
        {
            MessageBox.Show($"An error occurred:\n\n{errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowUnhandledExceptionDialog(object sender, UnhandledExceptionEventArgs e)
        {
            Exception exception = (Exception)e.ExceptionObject;
            HandleException(exception);
        }

        public static void ShowThreadExceptionDialog(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        public static void SetupUnhandledExceptionHandling()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += ShowThreadExceptionDialog;
            AppDomain.CurrentDomain.UnhandledException += ShowUnhandledExceptionDialog;
        }
    }
}