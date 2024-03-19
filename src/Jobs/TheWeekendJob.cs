using System;
using System.Threading.Tasks;
using Discord.Webhook;
using Quartz;

public class TheWeekendJob : IJob
{
    private readonly string TEST_WEBHOOK_URL = "https://discord.com/api/webhooks/848332481382973472/aWIVq0ZnRsu9lRUpsom2gM8iTKVUxfH4pFicjdDL8cyWmW0mcw57OrTvdT6gqeUo7-Ae";

    private readonly string WEBHOOK_URL = "https://discord.com/api/webhooks/849098362837925929/9jFCXj18fJzj8FKU-9eI3NNN6N7QigXmEij8z3DkWmU-IjOO-j3vGakBopMgvDiKYWvD";

    public async Task Execute(IJobExecutionContext context)
    {
        try{
            var client = new DiscordWebhookClient(WEBHOOK_URL);
            await client.SendMessageAsync(username: "The Weekend", avatarUrl: "https://upload.wikimedia.org/wikipedia/commons/7/7f/Daniel_Craig_-_Film_Premiere_%22Spectre%22_007_-_on_the_Red_Carpet_in_Berlin_%2822387409720%29_%28cropped%29.jpg", text: "https://media.giphy.com/media/utUEJY2cXzVvnrB152/giphy.gif");
       }
       catch(Exception ex){
           Console.WriteLine(ex.Message);
           Console.WriteLine(ex.StackTrace);
           Console.WriteLine(ex.Source);
       }
    }
}
