using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Quartz;
using RestSharp;

public class DailyCovidReport : IJob
{   
    private string URL_VACCINATION_DATA = "https://api.covid19tracker.ca/reports";
    private string EXPOSURES_URL = "https://www2.gnb.ca/content/gnb/en/corporate/promo/covid-19/potential_public_exposure.html";
    private string ZONE_ONE_XPATH = "/html/body/div/article/div/div[9]/div/div[1]/div/div[3]/div/ul/li";
    private readonly string WEBHOOK_URL = "https://discord.com/api/webhooks/849098362837925929/9jFCXj18fJzj8FKU-9eI3NNN6N7QigXmEij8z3DkWmU-IjOO-j3vGakBopMgvDiKYWvD";


    private async Task<ProvinceReport> GetProvincialVaccinationDataAsync(string province){
        string url = URL_VACCINATION_DATA + "/province/" + province + "?date=" + DateTime.Now.ToShortDateString();
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
        if(report.data.Count == 0 || report.data == null){
            url = URL_VACCINATION_DATA + "/province/" + province + "?date=" + DateTime.Now.AddDays(-1).ToShortDateString();
            client = new RestClient(url);
            request = new RestRequest(url, DataFormat.Json);
            response = await client.ExecuteAsync(request);
            report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
            return report;
        }
        else{

            return report;
        }
    }

     /// <summary>
    /// Gets a <c>List</c> of <c>List</c> of <c>string</c> with all the new exposures in Moncton asynchronously
    /// </summary>
    /// <returns>A <c>List</c> of <c>List</c> of <c>string</c> of all the new exposures in Moncton</returns>
    public async Task<List<List<string>>> GetNewExposuresAsync(){
        var web = new HtmlWeb();
        HtmlDocument doc = await web.LoadFromWebAsync(EXPOSURES_URL);
        var nodes = doc.DocumentNode.SelectNodes(ZONE_ONE_XPATH);
        List<string> exposures = new List<string>();
        foreach(HtmlNode li in nodes){
            if(li.InnerText.Contains("NEW", StringComparison.InvariantCulture)){
                string msg = li.InnerText.Replace("&nbsp;", " ");
                msg = msg.Replace("&amp;", "&");
                exposures.Add(msg);
            }
            
        }

        List<List<string>> list = new List<List<string>>();
        string message = String.Join(" ", exposures);

        if(message.Length > 4096){
            double amount = (double)(Convert.ToDouble(message.Length) / 4096.0);
            decimal numEmbeds = (decimal)Math.Ceiling(amount);

                for(int i = 0; i <= numEmbeds; i++){
                    int msgLength = 0;
                    int lengthCounter = 0;
                    List<string> myList = new List<string>();
                    for(int j = 0; j < exposures.Count; j++){
                        if(msgLength <= 4096){
                            string msg = exposures[j].Replace("&nbsp;"," ");
                            int temp = msg.Length;
                            msgLength += temp;

                            if(msgLength > 4096){
                                msgLength -= temp;
                                break;
                            }
                            else{
                                myList.Add(msg);
                                msgLength += msg.Length;
                                lengthCounter++;
                            }    
                        }
                    }

                exposures.RemoveRange(0,lengthCounter);
                list.Add(myList);
            }
        }
        else{
            list.Add(exposures);
        }
        return list;
    }

    private async Task<Embed> CreateDailyReport(ProvinceReport report){
        EmbedBuilder builder = new EmbedBuilder();
        List<List<string>> exposures = await GetNewExposuresAsync();
        string desc = "EXPOSURES: " + Environment.NewLine;
        foreach(List<string> s in exposures){
            foreach(string x in s){
                desc += x + Environment.NewLine;
            }
        }
        typeof(EmbedBuilder).GetField("_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(builder, desc); //This line of code bypasses the character limit set in Discord.Net as it hasn't been updated to accomodate the new limit.
        builder.WithTitle("New Brunswick Daily Covid Report ");
        builder.WithThumbnailUrl(DataLookups.flags[report.province]);
        builder.AddField("New Cases", report.data[0].change_cases.ToString() ?? "0");
        builder.AddField("New Deaths", report.data[0].change_fatalities.ToString() ?? "0");
        builder.AddField("New Hospitalizations", report.data[0].change_hospitalizations.ToString() ?? "0");
        builder.AddField("New ICU Cases", report.data[0].change_criticals.ToString() ?? "0");
        builder.AddField("New Recoveries", report.data[0].change_recoveries.ToString() ?? "0");
        builder.AddField("New Vaccinations (First Dose)", report.data[0].change_vaccinations);
        builder.AddField("New Fully Vaccinated", report.data[0].change_vaccinated);
        builder.AddField("First Dose Percentage", Calculator.CalculateFirstDosePercentage(report) + "%");
        builder.AddField("Fully Vaccinated Percentage", Calculator.CalculateFullyVaxxedPercentage(report) + "%");
        builder.WithFooter("Updated: " + DateTime.Now.ToShortDateString());
        return builder.Build();
    }

    public async Task Execute(IJobExecutionContext context)
    {
    //     try{
    //         ProvinceReport report = await GetProvincialVaccinationDataAsync("nb");
    //         List<List<string>> exposures = await GetNewExposuresAsync();
    //         string desc = "EXPOSURES: " + Environment.NewLine;
    //         foreach(List<string> s in exposures){
    //             foreach(string x in s){
    //                 desc += x + Environment.NewLine;
    //             }
    //         }
    //         var client = new DiscordWebhookClient(WEBHOOK_URL);
    //         var message = new DiscordMessage(
    //             "null",
    //             username: "Daily Covid Update",
    //             avatarUrl: "https://pbs.twimg.com/profile_images/378800000507124316/55c3d8fe616263f43f26621064273ba2_400x400.jpeg",
    //             tts: false,
    //             embeds: new[]
    //             {
    //                 new DiscordMessageEmbed(
    //                     title:"New Brunswick Covid Update - " + DateTime.Now.DayOfWeek,
    //                     description: desc,
    //                     fields: new[]
    //                     {
    //                         new DiscordMessageEmbedField("New Cases", report.data[0].change_cases.ToString() ?? "0"),
    //                         new DiscordMessageEmbedField("New Deaths", report.data[0].change_fatalities.ToString() ?? "0"),
    //                         new DiscordMessageEmbedField("New Hospitalizations", report.data[0].change_hospitalizations.ToString() ?? "0"),
    //                         new DiscordMessageEmbedField("New ICU Cases", report.data[0].change_criticals.ToString() ?? "0"),
    //                         new DiscordMessageEmbedField("New Recoveries", report.data[0].change_recoveries.ToString() ?? "0"),
    //                         new DiscordMessageEmbedField("New Vaccinations (First Dose)", report.data[0].change_vaccinations.ToString()),
    //                         new DiscordMessageEmbedField("New Fully Vaccinated", report.data[0].change_vaccinated.ToString()),
    //                         new DiscordMessageEmbedField("First Dose Percentage", Calculator.CalculateFirstDosePercentage(report) + "%"),
    //                         new DiscordMessageEmbedField("Fully Vaccinated Percentage", Calculator.CalculateFullyVaxxedPercentage(report) + "%")
    //                     },
    //                     footer: new DiscordMessageEmbedFooter("Updated: " + DateTime.Now.ToShortDateString()),
    //                     thumbnail: new DiscordMessageEmbedThumbnail(DataLookups.flags[report.province])
    //                 )
    //             }
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