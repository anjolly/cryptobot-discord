using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CryptoBot
{
    public class ClientManager
    {
        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public static void Main(string[] args)
            => new ClientManager().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _client.Log += Log;

            string tokenFile = Directory.GetCurrentDirectory() + "\\token.txt";
            var lines = File.ReadAllLines(tokenFile);
            string token = lines[0];

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();

            CommandHandler commandHandler = new CommandHandler(_client, _services, _commands);
            await commandHandler.InstallCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
