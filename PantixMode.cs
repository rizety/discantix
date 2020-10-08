using System;
using Pantix.Utilities;

namespace Discantix
{
    public class PantixMode
    {
        private static Commands commandModule = new Commands();
        
        public static void Load()
        {
            PantixConsole.WriteSlow("[Discantix] ", 3, ConsoleColor.DarkMagenta);
            PantixConsole.WriteSlow("Ready to go!", 2, ConsoleColor.White);
        }
        
        public static void OnModeLoaded()
        {
            while (Input()) ;
        }
        
        private static bool Input() 
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("discantix");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("> ");
            Console.ForegroundColor = ConsoleColor.Green;
            string input = Console.ReadLine();
            
            if (commandModule.commandFunctions.ContainsKey(input.Split(" ")[0]))
            {
                commandModule.commandFunctions[input.Split(" ")[0]](input.Split(" "));
                return true;
            }
            else if (input == "")
                return true;
            else if (input == "exit")
                return false;
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Pantix.Commands.AscendingBeep(500);
                Console.WriteLine("Invalid command");
                return true;
            }
        }
    }
}