using System.ComponentModel;
using System.Diagnostics;
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


    static async Task onCommand(SocketSlashCommand interaction)
    {
        switch (interaction.Data.Name.ToLower())
        {
            case "sosu":
                await interaction.RespondAsync("Sosu Sending...", ephemeral: true);
                long count = 1;
                interaction.Data.Options.Where(option => option.Name == "count").ToList()
                    .ForEach(option => { count = (long)option.Value; });
                count = Math.Clamp(count, 1, 15);
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

            case "nnaa":
                await interaction.RespondAsync("This is a test...", ephemeral: true);
                long count1 = 1;
                interaction.Data.Options.Where(option => option.Name == "count").ToList()
                    .ForEach(option => { count1 = (long)option.Value; });
                count1 = Math.Clamp(count1, 1, 5);
                var sent1 = new List<long>();
                for (long i = 0; i < count1; i++)
                {
                    int rng = new Random().Next(Images.Nnaa.Length);
                    while (sent1.Contains(rng))
                    {
                        rng = new Random().Next(Images.Nnaa.Length);
                    }

                    await interaction.Channel.SendMessageAsync(Images.Nnaa[rng]);
                    sent1.Add(rng);
                }

                break;
        }
    }
}
        
    
    