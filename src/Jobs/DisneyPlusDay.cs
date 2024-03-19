using System;
using System.Threading.Tasks;
using Quartz;

public class DisneyPlusDay : IJob
{
    private readonly string WEBHOOK_URL = "https://discord.com/api/webhooks/849098362837925929/9jFCXj18fJzj8FKU-9eI3NNN6N7QigXmEij8z3DkWmU-IjOO-j3vGakBopMgvDiKYWvD";
    public async Task Execute(IJobExecutionContext context)
    {
    //     DateTime date = new DateTime(2021,11,12);
    //     TimeSpan gap = date - DateTime.Now;
    //     int numWeeks = gap.Days;
    //     try{
    //         var client = new DiscordWebhookClient(WEBHOOK_URL);
    //         var message = new DiscordMessage(
    //             "There is " + numWeeks + " days until Disney+ Day (The day where Disney announces all of their new projects)",
    //             username: "Disney+ Day Reminder",
    //             avatarUrl: "https://cdn.vox-cdn.com/thumbor/nUQOhWe0sYt7OrWvf0vj8kj0YDw=/1400x788/filters:format(jpeg)/cdn.vox-cdn.com/uploads/chorus_asset/file/13412121/disneyplus.0.jpg",
    //             tts: false,
    //             embeds: null
    //         );

    //         await client.SendToDiscord(message);
    //     }
    //    catch(Exception ex){
    //        Console.WriteLine(ex.Message);
    //        Console.WriteLine(ex.StackTrace);
    //        Console.WriteLine(ex.Source);
    //    }
    }
}