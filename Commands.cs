using System;
using System.Collections.Generic;

namespace Discantix
{
    public class Commands
    {
        public Commands()
        {
            commandFunctions.Add("help", Help);
            commandFunctions.Add("init", InitializeBot);
        }

        public readonly string[] commandsList =
        {
            "help:;void:;shows this menu. differs depending on current mode",
            "init:;takes string parameter - bot token:;initializes a bot session"
        };

        public Dictionary<string, Func<string[], dynamic>> commandFunctions = new Dictionary<string, Func<string[], dynamic>>();

        dynamic Help(string[] args)
        {
            foreach (string i in commandsList)
            {
                string[] commandInfo = i.Split(":;");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(commandInfo[0]);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(" - ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(commandInfo[1] == "void" ? "no parameters: " : commandInfo[1] + ": ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(commandInfo[2]);
            }
            return null;
        }
        
        void InvalidArgs(int commandIndex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Invalid arguments: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string[] commandInfo = commandsList[commandIndex].Split(":;");
            Console.Write(commandInfo[0]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" - ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(commandInfo[2]);
            Pantix.Commands.DescendingBeep(1000);
        }

        dynamic InitializeBot(string[] args)
        {
            if (args.Length != 2)
            { InvalidArgs(6); return null; }
            BotManager bot = new BotManager(args[1]);
            try { bot.Initialize().ConfigureAwait(false).GetAwaiter().GetResult(); } catch { InvalidArgs(6); return null; }
            return null;
        }
    }
}
