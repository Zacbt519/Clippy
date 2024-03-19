// using System;
// using System.Threading.Tasks;
// using Discord.Interactions;

// public class VaccineModule: InteractionModuleBase<SocketInteractionContext>{

//     public InteractionService Commands { get; set; }

//     private CommandHandler _handler;

//     private readonly CovidAPIService _apiService;
//     private readonly CovidEmbedService _embedService;
//     private readonly LoggingService _log;

//     public VaccineModule(CommandHandler handler, CovidAPIService apiService, CovidEmbedService embedService, LoggingService log){
//         _handler = handler;
//         _apiService = apiService;
//         _embedService = embedService;
//         _log = log;
//     }

//     [SlashCommand("vaccines", "Returns vaccination information for the given date and location")]
//     public async Task GetVaccinesAsync(Location location = Location.ALL, DateTime date = new DateTime()){
//         try{
//             if(location == Location.ALL && date.Year == 0001){
//                 ProvinceReport report = await _apiService.GetNationalVaccinationDataAsync();
//                 await RespondAsync("", embed: _embedService.EmbedVaccineBuilder(report, DateTime.Now));
//             }
//             else if (location != Location.ALL && date.Year == 0001){
//                 ProvinceReport report = await _apiService.GetProvincialVaccinationDataAsync(ParamterValidation.ValidateParameterInput(location.ToString().ToLower()));
//                 if(location == Location.PEI){
//                     if(report.data == null || report.data.Count == 0){
//                         await RespondAsync("PEI does not often provide updated numbers, and no data could be found for them in the last 2 days");    
//                     }

//                 }
//                 await RespondAsync("", embed: _embedService.EmbedVaccineBuilder(report, report.last_updated));
//             }
//             else if(location == Location.ALL && date.Year != 0001){
//                 ProvinceReport report = await _apiService.GetNationalVaccinationDataAsync();
//                 await RespondAsync("", embed: _embedService.EmbedVaccineBuilder(report, date));
//             }
//             else{
//                 ProvinceReport report = await _apiService.GetProvincialVaccinationDataAsync(ParamterValidation.ValidateParameterInput(location.ToString().ToLower()), date);
//                 if(location == Location.PEI){
//                     if(report.data == null || report.data.Count == 0){
//                         await RespondAsync("PEI does not often provide updated numbers, and no data could be found for them in the last 2 days");
//                     }
//                 }
//                 await RespondAsync("", embed: _embedService.EmbedVaccineBuilder(report, date));
//             }
//         }
//         catch(Exception ex){
//             _log.Error( Context,ex.Message);
//         }

//     }
// }