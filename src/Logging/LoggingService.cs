using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.Webhook;
using Discord.WebSocket;
using Newtonsoft.Json;
using System;
using System.IO;

public class LoggingService{

    private ulong serverID;
    private ulong logChannelID;
    private readonly LoggingMessagingService _service;

    private string WEB_HOOK_URL = "https://discord.com/api/webhooks/854406048760070165/uoPr0ZU3KhdRh3SX_bypojHlwMnZPDcvcBPKfg7OAHf2au1CivpWvBz_Dlk_831aHpv0";
    public LoggingService(LoggingMessagingService service){
        _service = service;
        using(StreamReader r = File.OpenText("src/Resources/strings.json")){
            string json = r.ReadToEnd();
            dynamic values = JsonConvert.DeserializeObject(json);

            serverID = (ulong)values.serverID;
            logChannelID = (ulong)values.logChannelID;
        }
    }

    private SocketGuildChannel GetChannel(SocketInteractionContext Context){
        var client = Context.Client;
        return client.GetGuild(serverID).GetChannel(logChannelID);
    }
    public async void Log(LogMessage log){
        try{
            var client = new DiscordWebhookClient(WEB_HOOK_URL);
            var embed = _service.Log(log);

            var msg = new EmbedBuilder{
                Title = embed.Title,
                Description = embed.Description
            };
        
           

        await client.SendMessageAsync(embeds: new[] { msg.Build() });
       }
       catch(Exception ex){
           Console.WriteLine(ex.Message);
           Console.WriteLine(ex.StackTrace);
           Console.WriteLine(ex.Source);
       }
    }

    public async void Log(SocketInteractionContext Context, string msg){
        var guild = Context.Guild;
        var channel = guild.GetTextChannel(logChannelID);
        await channel.SendMessageAsync("", false,_service.Log(msg));
    }

    public async void Debug(SocketInteractionContext Context, string msg){
        var guild = Context.Guild;
        var channel = guild.GetTextChannel(logChannelID);
        await channel.SendMessageAsync("", false,_service.LogDebug(msg));
    }

    public async void Verbose(SocketInteractionContext Context, string msg){
        var guild = Context.Guild;
        var channel = guild.GetTextChannel(logChannelID);
        await channel.SendMessageAsync("", false,_service.LogVerbose(msg));
    }

    public async void Info(SocketInteractionContext Context, string msg){
        var guild = Context.Guild;
        var channel = guild.GetTextChannel(logChannelID);
        await channel.SendMessageAsync("", false,_service.LogInfo(msg));
    }

    public async void Error(SocketInteractionContext Context, string msg){
        var guild = Context.Guild;
        var channel = guild.GetTextChannel(logChannelID);
        await channel.SendMessageAsync("", false,_service.LogError(msg));
    }

    public async void Critical(SocketInteractionContext Context, string msg){
        var guild = Context.Guild;
        var channel = guild.GetTextChannel(logChannelID);
        await channel.SendMessageAsync("", false,_service.LogCritical(msg));
    }

}