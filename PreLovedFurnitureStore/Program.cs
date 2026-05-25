using System;
using System.Windows.Forms;

namespace PreLovedFurnitureStore
{
    /// <summary>
    /// Main entry point for the Pre-Loved Furniture Store application.
    /// Initializes the application and displays the Welcome Form.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WelcomeForm());
        }
    }
}
