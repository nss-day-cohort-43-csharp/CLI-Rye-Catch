using TabloidCLI.UserInterfaceManagers;
using System;
namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             list of possible console colors:
            Black
            Blue
            Cyan	
            DarkBlue	
            DarkCyan		
            DarkGray		
            DarkGreen		
            DarkMagenta		
            DarkRed		
            DarkYellow	
            Gray		
            Green		
            Magenta		
            Red	
            White
            Yellow
             */
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.Clear();
            Console.WriteLine("Welcome to the Tabloid");
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
