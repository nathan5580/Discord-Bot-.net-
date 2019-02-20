using Discord;
using Discord.WebSocket;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace Monara
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Mains the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task MainAsync()
        {
            var client = new DiscordSocketClient();

            client.Log += Log;
            client.MessageReceived += MessageReceived;

            string token = ConfigurationManager.AppSettings["DiscordToken"];
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private async Task MessageReceived(SocketMessage message)
        {
            switch (message.Content)
            {
                case "!ping":
                    await message.Channel.SendMessageAsync("Pong!");
                    break;
                case "!help":
                    await message.Channel.SendMessageAsync("Besoin d'aide ? Va voir ailleurs !");
                    break;
                default:
                    if (message.Content.StartsWith('!'))
                    {
                        await message.Channel.SendMessageAsync("J'te comprend pas :(");
                    }
                    break;
            }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
