using DatabaseManager.Controllers;
using DatabaseManager.Models;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.VoiceNext;
using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AndriaBot
{
    public class FunCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Comando para verificar o ping")]
        public async Task Ping(CommandContext ctx)
        {
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");
            await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} {emoji} Pong! Ping: {ctx.Client.Ping}ms");
        }


        [Command("calc")]
        [Description("Realiza uma operação aritmética entre dois números")]
        [Aliases("Calculadora", "Calcular")]
        public async Task Calc(CommandContext ctx, [Description("Primeiro Operando")] decimal numberOne, [Description("Operador")] string operat, [Description("Segundo Operando")] decimal numberTwo)
        {
            decimal numberReturn = 0;
            switch (operat)
            {
                case "+":
                    numberReturn = numberOne + numberTwo;
                    break;
                case "-":
                    numberReturn = numberOne - numberTwo;
                    break;
                case "/":
                    numberReturn = numberOne / numberTwo;
                    break;
                case "*":
                    numberReturn = numberOne * numberTwo;
                    break;
                case "somar":
                    numberReturn = numberOne + numberTwo;
                    break;
                case "subtrair":
                    numberReturn = numberOne - numberTwo;
                    break;
                case "dividir":
                    numberReturn = numberOne / numberTwo;
                    break;
                case "multiplicar":
                    numberReturn = numberOne * numberTwo;
                    break;
                case "sub":
                    numberReturn = numberOne + numberTwo;
                    break;
                case "add":
                    numberReturn = numberOne - numberTwo;
                    break;
                case "div":
                    numberReturn = numberOne / numberTwo;
                    break;
                case "mul":
                    numberReturn = numberOne * numberTwo;
                    break;

            }
            await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} {numberReturn.ToString()}").ConfigureAwait(false);
        }


        [Command("bit"), Description("Converte um número decimal em um número binário")]
        public async Task getBit(CommandContext ctx, [Description("Número a ser convertido")] int number)
        {
            string binary = Convert.ToString(number, 2);
            await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} {binary}").ConfigureAwait(false);
        }

        
        [Command("unmute")]
        [RequireUserPermissions(Permissions.MuteMembers)]
        [Description("Desmuta o usuário mencionado")]
        public async Task unmuteUser(CommandContext ctx,[Description("Membro a ser desmutado")] DiscordMember member)
        {
            await member.SetMuteAsync(false);
            await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} {member.Username} Foi desmutado!");
        }


        [Command("mute")]
        [RequireUserPermissions(Permissions.MuteMembers)]
        [Description("Muta o usuário mencionado")]
        public async Task muteUser(CommandContext ctx, DiscordMember member, int minutes = 0)
        {
            await member.SetMuteAsync(true);

            if (minutes == 0)
            {
                await ctx.RespondAsync(member.Username + " Foi mutado");
                return;
            }
            await ctx.RespondAsync(member.Username + " Foi mutado por " + minutes + (minutes == 1 ? " minuto" : " minutos"));
            await Task.Delay(minutes * 60000);
            await member.SetMuteAsync(false);
            await ctx.RespondAsync(member.Username + " Foi desmutado!");
        }


        [Command("Traduzir")]
        [Aliases("Translate", "t")]
        [Description("Traduz o texto do inglês para o português")]
        public async Task Traduzir(CommandContext ctx,[Description("Texto a ser traduzido")] [RemainingText] string text)
        {
            string result = Tradutor.Traduzir(text);
            await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} {result}");
        }

        [Command("MuteAll")]
        [Aliases("ma","m")]
        [Description("Muta todos os usuários que estiverem no mesmo canal de voz que você")]
        [RequirePermissions(Permissions.MuteMembers)]
        public async Task MuteAll(CommandContext ctx)
        {
            if (ctx.Guild == null)
            {
                await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} Você não está em um servidor :O").ConfigureAwait(false);
                return;
            }
            var vnext = ctx.Client.GetVoiceNext();
            var vnc = vnext.GetConnection(ctx.Guild);

            if (vnc == null)
            {
                var chn = ctx.Member?.VoiceState?.Channel;
                if (chn == null)
                {
                    await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} Você precisa estar em um canal de voz para usar esse comando");
                    return;
                }
            }
            if(ctx.Member?.VoiceState?.Channel.Users != null)
                foreach(var member in ctx.Member?.VoiceState?.Channel.Users)
                {
                    if (!member.IsBot)
                        await member.SetMuteAsync(true);
                }
            await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} Todos os usuários foram mutados!").ConfigureAwait(false);
        }

        [Command("UnMuteAll")]
        [Aliases("uma", "um")]
        [Description("Desmuta todos os usuários que estiverem no mesmo canal de voz que você")]
        [RequirePermissions(Permissions.MuteMembers)]
        public async Task UnMuteAll(CommandContext ctx)
        {
            if (ctx.Member?.VoiceState?.Channel.Users != null)
                foreach (var member in ctx.Member?.VoiceState?.Channel.Users)
                {
                    if (!member.IsBot)
                        await member.SetMuteAsync(false);
                }
            else
                await ctx.RespondAsync("Error");
            await ctx.Channel.SendMessageAsync($"{ctx.User.Mention} Todos os usuários foram desmutados!");
        }

        [Command("setText")]
        [Aliases("ST")]
        [Description("Comando para alterar o canal padrão onde o bot envia as mensagens\n Uso: .setText <Channel Mention>")]
        public async Task setTextChannel(CommandContext ctx, [Description("Canal de texto")] DiscordChannel channel)
        {
            if (channel.Type != ChannelType.Text)
            {
                await ctx.RespondAsync("O canal precisa ser do tipo Texto!");
                return;
            }
            GuildText guildText = new GuildText() { GuildID = ctx.Guild.Id.ToString() };
            await guildText.Select();
            if (string.IsNullOrEmpty(guildText.TextChannelID) || guildText.TextChannelID == "0")
            {
                guildText.TextChannelID = channel.Id.ToString(); ;
                await guildText.Insert();
            }
            else
            {
                guildText.TextChannelID = channel.Id.ToString();
                await guildText.Update();
            }
            DiscordMessage response = await ctx.Channel.SendMessageAsync(ctx.User.Mention + " Positivo e operante , agora só falarei aqui! " + channel.Mention);
            await Task.Delay(10000);
            await ctx.Message.DeleteAsync();
        }

        [Command("DisableChannel")]
        [Aliases("DChannel", "DC")]
        [Description("Habilita o controle de envio mensagens a um canal da guilda")]
        public async Task ChannelDisable(CommandContext ctx, [Description("Uma Canal de texto do discord")] DiscordChannel channel)
        {
            DisabledChannels disable = new DisabledChannels(ctx.Guild.Id.ToString(CultureInfo.InvariantCulture), channel.Id.ToString(CultureInfo.InvariantCulture));
            await disable.Select();
            if (disable.isDisabled)
            {
                await ctx.RespondAsync($"O controle de mensagens do canal {channel.Name} já está habilitado");
            }
            else
            {
                await disable.Insert();
                await ctx.RespondAsync($"O controle de mensagens do canal {channel.Name} foi habilitado");
            }
        }
        [Command("EnableChannel")]
        [Aliases("EChannel", "EC")]
        [Description("Desabilita o controle de mensagens a um canal da guilda")]
        public async Task ChannelEnable(CommandContext ctx, [Description("Uma Canal de texto do discord")] DiscordChannel channel)
        {
            DisabledChannels disable = new DisabledChannels(ctx.Guild.Id.ToString(CultureInfo.InvariantCulture), channel.Id.ToString(CultureInfo.InvariantCulture));
            await disable.Select();
            if(disable.isDisabled)
            {
                await disable.Delete();
                await ctx.RespondAsync($"O controle de mensagens do canal {channel.Name} foi desabilitado");
            }
            else
            {
                await ctx.RespondAsync($"O controle de mensagens do canal {channel.Name} não está habilitado");
            }

        }

    }
}
