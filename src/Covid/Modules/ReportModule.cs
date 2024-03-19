// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Discord.Interactions;

// public class ReportModule : InteractionModuleBase<SocketInteractionContext>{
//     public InteractionService Commands { get; set; }
//     private CommandHandler _handler;
//     private readonly CovidEmbedService _embedService;
//     private readonly CovidWebScraperService _webScraperService;
//     private readonly CovidAPIService _apiService;
//     private readonly LoggingService _log;

//     public ReportModule(CommandHandler handler, CovidEmbedService embedService, CovidWebScraperService webScraperService, CovidAPIService aPIService, LoggingService log){
//         _handler = handler;
//         _embedService = embedService;
//         _webScraperService = webScraperService;
//         _apiService = aPIService;
//         _log = log;
//     }

//     [SlashCommand("report", "Retrieves a roundup of todays covid information")]
//     public async Task GetReportAsync(){
//         try{
//             List<int> breakdown = new List<int>();
//             breakdown.Add(await _apiService.GetZoneBreakdown(1301));
//             breakdown.Add(await _apiService.GetZoneBreakdown(1302));
//             breakdown.Add(await _apiService.GetZoneBreakdown(1303));
//             breakdown.Add(await _apiService.GetZoneBreakdown(1304));
//             breakdown.Add(await _apiService.GetZoneBreakdown(1305));
//             breakdown.Add(await _apiService.GetZoneBreakdown(1306));
//             breakdown.Add(await _apiService.GetZoneBreakdown(1307));
//             ProvinceReport report = await _apiService.GetProvincialCovidDataAsync("nb", DateTime.Now);
//             await RespondAsync(embed: _embedService.EmbedReportBuilder(report, breakdown));
//         }
//         catch(Exception ex){
//             _log.Error( Context,ex.Message);
//         }
//     }


// }