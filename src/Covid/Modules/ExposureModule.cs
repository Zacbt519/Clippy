// using System;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Discord;
// using Discord.Interactions;
// using TT6Exceptions;

// public class ExposureModule : InteractionModuleBase<SocketInteractionContext>{
//     public InteractionService Commands { get; set; }

//     private CommandHandler _handler;
//     private readonly CovidEmbedService _embedService;

//     private readonly CovidWebScraperService _webScraperService; 

//     private readonly LoggingService _log;


//     public ExposureModule(CommandHandler handler, CovidEmbedService embedService, CovidWebScraperService scraperService, LoggingService log){
//         _handler = handler;
//         _embedService = embedService;
//         _webScraperService = scraperService;
//         _log = log;
//     }

//     [SlashCommand("exposures", "Returns a list of new or old covid exposures in Zone 1 (Moncton)")]
//     public async Task GetExposuresAsync(ExposureChoices choice){
//         try{
//             if(choice == ExposureChoices.New){
//                 List<List<string>> exposures = await _webScraperService.GetNewestExposuresAsync();
//                 List<Embed> embeds = _embedService.EmbedExposureBuilder(exposures, await _webScraperService.GetDateAsync());
//                 Embed[] embedArray = embeds.ToArray();
//                 await RespondAsync("", embedArray);
//             }
//             else{
//                 List<List<string>> exposures = await _webScraperService.GetPastExposuresAsync();
//                 List<Embed> embeds = _embedService.EmbedExposureBuilder(exposures, await _webScraperService.GetDateAsync());
//                 Embed[] embedArray = embeds.ToArray();
//                 await RespondAsync("", embedArray);
//             }
//         }
//         catch(Exception ex){
//             _log.Error( Context,ex.Message);
//             if(ex is ApiException){
//                 await RespondAsync("No New Exposures");
//             }

//         }

//     }
// }