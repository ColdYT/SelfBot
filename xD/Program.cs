using System;
using System.Threading;
using Discord;
using Discord.Gateway;
using System.Drawing;

namespace xD
{
    class Program
    {
        static void Main(string[] args)
        {
            #region
            string Token = "NTIzMTEyMzI5NDQ2MDMxMzc5.Xe04Kg.bcMMIWHpNAqwR6wUdgDam7FWKqE";
            #endregion

            DiscordSocketClient Client = new DiscordSocketClient();

            Client.Login(Token);

            Client.OnLoggedIn += Client_OnLoggedIn;
            Client.OnMessageReceived += Client_OnMessageReceived;

            Console.Title = $"Now using {Client.User.Username}";

            Thread.Sleep(-1);
        }

        private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            Console.WriteLine($"{args.Message.Author} : {args.Message.Content}");

            if (args.Message.Author.User.Id != client.User.Id) return;

            if (args.Message.Content.Split(' ')[0] != "+pfp") return;

            if (args.Message.Mentions.Count == 0)
                SendPFP(client, client.User, args.Message.ChannelId);
            else
                SendPFP(client, args.Message.Mentions[0], args.Message.ChannelId);
        }

        private static void SendPFP(DiscordSocketClient client, User user, ulong ChannelId)
        {
            EmbedMaker embed = new EmbedMaker();

            embed.Title = $"PFP of {user.Username}";
            embed.Color = Color.FromArgb(0, 100, 200);
            embed.ImageUrl = $"https://cdn.discordapp.com/avatars/{user.Id}/{user.AvatarId}.png";

            client.SendMessage(ChannelId, "", false, embed);
        }

        private static void Client_OnLoggedIn(DiscordSocketClient client, LoginEventArgs args)
        {
            Console.WriteLine($"Logged into {args.User.Username}");
        }
    }
}
