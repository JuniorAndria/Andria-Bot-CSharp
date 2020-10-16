using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.VoiceNext;
using DSPlus.Examples;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AndriaBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public VoiceNextExtension Voice { get; private set; }
        public VoiceNextConfiguration Vnc  { get; private set; }
       
        public async Task RunAsync()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);
            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);
            var config = new DiscordConfiguration()
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };
            Client = new DiscordClient(config);
            Voice = Client.UseVoiceNext();
            Client.Ready += OnClientReady;
            string[] prefixes;
            if (configJson.Prefix.Contains(" "))
            {
                prefixes = configJson.Prefix.Split(" ");
            }
            else
            {
                prefixes = new string[] { configJson.Prefix };
            }
            var CommandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = prefixes,
                EnableDms = true,
                EnableMentionPrefix = true,
                DmHelp = false
            };
            Commands = Client.UseCommandsNext(CommandsConfig);
            Commands.RegisterCommands<FunCommands>();
            //Commands.RegisterCommands<MusCommands>(); //Not Working for this time
            DiscordActivity activity = new DiscordActivity(".help | www.andria.dev", ActivityType.Streaming);
            await Client.ConnectAsync(activity);
            await Task.Delay(-1);   


        }
        private Task OnClientReady(DiscordClient sender, ReadyEventArgs args)
        {
            Console.WriteLine("Iniciado!");
            return Task.CompletedTask;
        }
    }
}
