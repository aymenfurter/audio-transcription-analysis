using System;
using System.Windows.Forms;

namespace AudioTranscriptionAnalysis
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var userInterfaceModule = new UserInterfaceModule();
            userInterfaceModule.ShowUI();
        }
    }
}