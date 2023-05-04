using System.ComponentModel;
using System.Net.Mime;
using Discord;
using Discord.WebSocket;
using FapuButt;

public static class Faputa
{
    private static DiscordSocketClient client;

        public static async Task Main(string[] args)
    {
        await Run();
    }

    private static async Task Run()
    {
        string token = await File.ReadAllTextAsync("token.txt");
        client = new DiscordSocketClient();
        client.Log += Logger.LogAsync;
        client.Ready += Ready;
        client.SlashCommandExecuted += onCommand;
        await client.LoginAsync(TokenType.Bot, token);
        await client.StartAsync();
        await Task.Delay(-1);
        
    }

    private static async Task Ready()
    {
        Logger.Log("Ready");
        var sosuCommand = new SlashCommandBuilder()
        {
            Name = "sosu",
            Description = "Fapu"
        };
        sosuCommand.AddOption(new SlashCommandOptionBuilder()
        {
            Name = "count",
            Description = "Amount of Fapu",
            Type = ApplicationCommandOptionType.Integer
        });
            await client.CreateGlobalApplicationCommandAsync(sosuCommand.Build());
    }

    private static async Task onCommand(SocketSlashCommand interaction)
    {
        switch (interaction.Data.Name.ToLower())
        {
            case "sosu":
                await interaction.RespondAsync("Sosu Sending...", ephemeral:true);
                long count = 1;
                interaction.Data.Options.Where(option => option.Name == "count").ToList().ForEach(option => {
                    count = (long)option.Value;
                });
                count = Math.Clamp(count, 1, 10);
                var sent = new List<long>();
                for (long i = 0; i < count; i++)
                {
                    int rng = new Random().Next(Images.Sosu.Length);
                    while (sent.Contains(rng))
                    {
                        rng = new Random().Next(Images.Sosu.Length);
                    }
                    await interaction.Channel.SendMessageAsync(Images.Sosu[rng]);
                    sent.Add(rng);
                }
                break;
        }
    }
}