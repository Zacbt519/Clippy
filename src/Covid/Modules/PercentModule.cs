// using System;
// using System.Threading.Tasks;
// using Discord.Interactions;

// public class PercentModule : InteractionModuleBase<SocketInteractionContext>{
//     public InteractionService Commands { get; set; }

//     private CommandHandler _handler;

//     private readonly CovidAPIService _apiService;
//     private readonly CovidEmbedService _embedService;
//     private readonly LoggingService _log;

//     public PercentModule(CommandHandler handler, CovidAPIService apiService, CovidEmbedService embedService, LoggingService log){
//         _handler = handler;
//         _apiService = apiService;
//         _embedService = embedService;
//         _log = log;
//     }

//     [SlashCommand("percent", "Returns the percentage of people vaccinated per given region and date")]
//     public async Task GetPercentageAsync(Location location = Location.ALL, DateTime date = new DateTime()){
//         try{
//             if(location == Location.ALL && date.Year == 0001){
//                 ProvinceReport report = await _apiService.GetNationalVaccinationDataAsync();
//                 await RespondAsync("", embed: _embedService.EmbedPercentBuilder(report, DateTime.Now));
//             }
//             else if (location != Location.ALL && date.Year == 0001){
//                 if(ParamterValidation.IsValidProvinceCode(location.ToString().ToLower())){
//                     ProvinceReport report = await _apiService.GetProvincialVaccinationDataAsync(location.ToString().ToLower());
//                     if(location == Location.PEI){
//                         if(report.data == null){
//                             await RespondAsync("PEI does not often provide updated numbers, and no data could be found for them in the last 2 days");
//                         }
//                     }
//                     await RespondAsync("", embed: _embedService.EmbedPercentBuilder(report, DateTime.Now));
//                 }
//             }
//             else if(location == Location.ALL && date.Year != 0001){
//                 ProvinceReport report = await _apiService.GetNationalVaccinationDataAsync(date);
//                 await RespondAsync("", embed: _embedService.EmbedPercentBuilder(report, date));
//             }
//             else{
//                 if(ParamterValidation.IsValidProvinceCode(location.ToString().ToLower())){
//                     ProvinceReport report = await _apiService.GetProvincialVaccinationDataAsync(location.ToString().ToLower(), date);
//                     if(location == Location.PEI){
//                         if(report.data == null){
//                             await RespondAsync("PEI does not often provide updated numbers, and no data could be found for them in the last 2 days");
//                         }
//                     }
//                     await RespondAsync("", embed: _embedService.EmbedPercentBuilder(report, date));
//                 }
//             }
//         }
//         catch(Exception ex){
//             _log.Error( Context,ex.Message);
//         }
//     }

// }