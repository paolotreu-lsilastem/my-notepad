using System.Diagnostics;

namespace MyNotepad;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        if (args.Length == 1)
        {
            Application.Run(new Form1(args[0]));
        }
        else if (args.Length > 1)
        {
            foreach (var file in args)
            {
                Process.Start(Environment.ProcessPath!, new string[] { file });
            }
        }
        else
        {
            Application.Run(new Form1());
        }
    }    
}