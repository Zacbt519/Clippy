using System;
using System.Threading.Tasks;
using Discord.Webhook;
using Quartz;

public class RibFestJob : IJob
{
    private readonly string TEST_WEBHOOK_URL = "https://discord.com/api/webhooks/848332481382973472/aWIVq0ZnRsu9lRUpsom2gM8iTKVUxfH4pFicjdDL8cyWmW0mcw57OrTvdT6gqeUo7-Ae";

    private readonly string WEBHOOK_URL = "https://discord.com/api/webhooks/849098362837925929/9jFCXj18fJzj8FKU-9eI3NNN6N7QigXmEij8z3DkWmU-IjOO-j3vGakBopMgvDiKYWvD";

    private string CalculateDaysUntil()
    {
        return (new DateTime(2022, 06, 24) - DateTime.Now).Days.ToString();
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var client = new DiscordWebhookClient(WEBHOOK_URL);
            await client.SendMessageAsync(username: "Ribfest Countdown", avatarUrl: "https://rotaryribfestmoncton.ca/wp-content/uploads/2022/03/ribfest-web-2018_logo_0.png", text: "Only " + CalculateDaysUntil() + " days until Ribfest!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine(ex.Source);
        }
    }
}