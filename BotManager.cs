using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Pantix.Utilities;
using DSharpPlus;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Discantix
{
    public class BotManager
    {
        public BotManager(string Token)
        {
            this.Token = Token;
        }

        public string Token { get; private set; }

        public async Task Initialize()
        {
            commandModule = new Commands();

            discord = new DiscordClient(new DiscordConfiguration
            {
                Token = Token,
                TokenType = TokenType.Bot
            });

            await discord.ConnectAsync();

            discord.MessageCreated += async e =>
            {
                PantixConsole.WriteSlow("\nMessage: ", 3, ConsoleColor.Green);
                PantixConsole.WriteSlow(e.Message.Content, 5, ConsoleColor.White);
                PantixConsole.WriteLineSlow($" [{e.Message.Author.Username}#{e.Message.Author.Discriminator}]", 5, ConsoleColor.Cyan);
                PantixConsole.WriteSlow("dbot", 0, ConsoleColor.DarkYellow);
                PantixConsole.WriteSlow("> ", 0, ConsoleColor.DarkMagenta);
            };

            new Thread(Input).Start();

            await Task.Delay(-1);
        }

        private DiscordClient discord;

        private Commands commandModule;

        private void Input()
        {
            while (true)
            {
                PantixConsole.WriteSlow("dbot", 0, ConsoleColor.DarkYellow);
                PantixConsole.WriteSlow("> ", 0, ConsoleColor.DarkMagenta);
                string input = Console.ReadLine();

                if (commandModule.commandFunctions.ContainsKey(input.Split(" ")[0]))
                {
                    foreach (string i in commandModule.commandsList)
                    {
                        commandModule.commandFunctions[input.Split(" ")[0]](input.Split(" "));
                    }

                    Console.ForegroundColor = ConsoleColor.Red;
                    Pantix.Commands.AscendingBeep(500);
                    Console.WriteLine("Invalid command");

                    return;
                }
                else if (input == "")
                    return;
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Pantix.Commands.AscendingBeep(500);
                    Console.WriteLine("Invalid command");
                }
            }
        }
    }
}
