using System;
using System.Threading;
using Discord;
using Discord.Gateway;
using System.Drawing;
using Newtonsoft.Json;
using System.IO;

namespace xD
{
    class Program
    {

        private static string Prefix;
        static void Main(string[] args)
        {

            Console.Title = "Selfbot - By Xaxlii";

            Config config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("Config.json"));
            Prefix = (string)config.Prefix;

            while (string.IsNullOrEmpty(config.Prefix))
            {
                Console.Write("Prefix: ");
                config.Prefix = Console.ReadLine();
            }

            File.WriteAllText("Config.json", JsonConvert.SerializeObject(config));

            Console.Clear();

            ConfigToken configtoken = JsonConvert.DeserializeObject<ConfigToken>(File.ReadAllText("ConfigToken.json"));

            while (string.IsNullOrEmpty(configtoken.Token))
            {
                Console.Write("Your Discord token: ");
                configtoken.Token = Console.ReadLine();
            }

            File.WriteAllText("ConfigToken.Json", JsonConvert.SerializeObject(configtoken));

            DiscordSocketClient Client = new DiscordSocketClient();

            try
            {
                Client.Login(configtoken.Token);
            } catch (InvalidTokenException)
            {
                Console.WriteLine("Incorrect Token");
            }


            Client.OnLoggedIn += Client_OnLoggedIn;
            Client.OnMessageReceived += Client_OnMessageReceived;

            Console.Title = $"Now using {Client.User.Username} - Selfbot by Xaxlii";

            Thread.Sleep(-1);
        }

        private static void Client_OnMessageReceived(DiscordSocketClient client, MessageEventArgs args)
        {
            if (args.Message.Author.User.Id != client.User.Id) return;

            if (args.Message.Content.Split(' ')[0] != Prefix + "pfp") return;

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
            Console.Clear();
            Console.WriteLine($"Logged into {args.User.Username}");
        }
    }
}
