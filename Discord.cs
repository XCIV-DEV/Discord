using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;

// these are example code to execute what ever that goes on discord channel and send to your rage server
namespace NeptuneEvo.Core
{
    public class Program
    {
        private DiscordSocketClient _client;
        private ulong _channelId = YOUR_CHANNEL_ID; // eg.1234567484131651684765

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        //this has to run when you start the server do what ever that fit your server, make it run at the first place when you started ragemp-server.exe
        [ServerEvent(Event.ResourceStart)]
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            _client.Log += LogAsync;
            _client.Ready += ReadyAsync;

            await _client.LoginAsync(TokenType.Bot, "YOUR_BOT_TOKEN"); // make it here https // discord.com/developers/applications then invite to your discord as an administrator permission
            await _client.StartAsync();

            _client.MessageReceived += MessageReceivedAsync;

            await Task.Delay(-1);
        }

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            // Check if the event handler for message received is null
            if (message == null)
            {
                // Event handler is null, so don't process the message
                return;
            }

            // Check if the message is from the desired channel
            if (message.Channel is SocketTextChannel textChannel && textChannel.Id == _channelId)
            {
                // Your codes here eg. to announce or what ever
                Console.WriteLine($"~g~[From Discord]:{message.Author.Username}: {message.Content}"); // this is a print to console
            }
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log);
            return Task.CompletedTask;
        }

        private async Task ReadyAsync()
        {
            Console.WriteLine($"{_client.CurrentUser.Username} is connected and ready!"); // this will print out your discord bot username app make sure that username is corect.
        }
    }
}