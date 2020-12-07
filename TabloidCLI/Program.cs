using TabloidCLI.UserInterfaceManagers;
using System;
namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to the Blog");
            System.Threading.Thread.Sleep(5000);
            Console.Clear();
            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
