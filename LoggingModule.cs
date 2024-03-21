using System;
using System.IO;

namespace AudioTranscriptionAnalysis
{
    public static class LoggingModule
    {
        private static string logFilePath;

        public static void Initialize()
        {
            logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "app.log");
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath));
        }

        public static void LogInfo(string message)
        {
            Log("INFO", message);
        }

        public static void LogWarning(string message)
        {
            Log("WARNING", message);
        }

        public static void LogError(string message)
        {
            Log("ERROR", message);
        }

        public static void LogException(Exception exception)
        {
            Log("EXCEPTION", $"{exception.Message}\n{exception.StackTrace}");
        }

        private static void Log(string level, string message)
        {
            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}";
            Console.WriteLine(logMessage);

            try
            {
                File.AppendAllText(logFilePath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write log message to file: {ex.Message}");
            }
        }
    }
}