using System;
using System.Threading.Tasks;
using Discord.Webhook;
using Quartz;

public class LottoJob : IJob
{
    private readonly string WEBHOOK_URL = "https://discord.com/api/webhooks/849098362837925929/9jFCXj18fJzj8FKU-9eI3NNN6N7QigXmEij8z3DkWmU-IjOO-j3vGakBopMgvDiKYWvD";
    public async Task Execute(IJobExecutionContext context)
    {
        try{
            var client = new DiscordWebhookClient(WEBHOOK_URL);
            await client.SendMessageAsync(avatarUrl:"https://www.alc.ca/content/dam/alc/images/static/Atlantic_Lottery_Corporation.svg.png", username:"Lotto Reminder", text: "@everyone This is your reminder to send $10 to Maisey for the Lottery Pool" );
        }
       catch(Exception ex){
           Console.WriteLine(ex.Message);
           Console.WriteLine(ex.StackTrace);
           Console.WriteLine(ex.Source);
       }
    }
}