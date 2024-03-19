using System.Threading.Tasks;
using Quartz;
using Discord;
using Discord.WebSocket;
using System;
using Newtonsoft.Json;
using Discord.Commands;
using Discord.Webhook;

public class WednesdayJob : IJob
{
    private readonly string TEST_WEBHOOK_URL = "https://discord.com/api/webhooks/848332481382973472/aWIVq0ZnRsu9lRUpsom2gM8iTKVUxfH4pFicjdDL8cyWmW0mcw57OrTvdT6gqeUo7-Ae";
    private readonly string WEBHOOK_URL = "https://discord.com/api/webhooks/849098362837925929/9jFCXj18fJzj8FKU-9eI3NNN6N7QigXmEij8z3DkWmU-IjOO-j3vGakBopMgvDiKYWvD";

    public async Task Execute(IJobExecutionContext context)
    {
       try{
        var client = new DiscordWebhookClient(WEBHOOK_URL);
        await client.SendMessageAsync(username:"Wednesday", avatarUrl:"https://i1.sndcdn.com/artworks-000161731035-kbrz4r-t500x500.jpg", text: "https://cdn.discordapp.com/attachments/642132114086690834/847092359506427904/frog.jpg");
       }
       catch(Exception ex){
           Console.WriteLine(ex.Message);
           Console.WriteLine(ex.StackTrace);
           Console.WriteLine(ex.Source);
       }
        
    }

}