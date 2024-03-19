// using System;
// using System.Threading.Tasks;
// using Discord.Interactions;

// public class CaseModule : InteractionModuleBase<SocketInteractionContext>{
//     public InteractionService Commands { get; set; }

//     private CommandHandler _handler;

//     private readonly CovidAPIService _apiService;
//     private readonly CovidEmbedService _embedService;
//     private readonly LoggingService _log;

//     public CaseModule(CommandHandler handler, CovidAPIService apiService, CovidEmbedService embedService, LoggingService log){
//         _handler = handler;
//         _apiService = apiService;
//         _embedService = embedService;
//         _log = log;
//     }

//     // Slash Commands are declared using the [SlashCommand], you need to provide a name and a description, both following the Discord guidelines
//         [SlashCommand("cases", "Returns case information for the given date and location")]
//         // By setting the DefaultPermission to false, you can disable the command by default. No one can use the command until you give them permission
//         [DefaultPermission(true)]
//         public async Task GetCasesAsync (Location location = Location.ALL, DateTime date = new DateTime())
//         {
//             try{

//                 if(location == Location.ALL && date.Year == 0001){
//                     ProvinceReport report = await _apiService.GetNationalCovidCasesAsync(DateTime.Now);
//                     await RespondAsync("", embed: _embedService.EmbedCaseBuilder(report, DateTime.Now));
//                 }
//                 else if (location != Location.ALL && date.Year == 0001){
//                     ProvinceReport report = await _apiService.GetProvincialCovidDataAsync(ParamterValidation.ValidateParameterInput(location.ToString().ToLower()), DateTime.Now);
//                     if(location == Location.PEI){
//                         if(report.data == null || report.data.Count == 0){
//                             await RespondAsync("PEI does not often provide updated numbers, and no data could be found for them in the last 2 days");
//                         }
//                     }
//                     await RespondAsync("", embed: _embedService.EmbedCaseBuilder(report, DateTime.Now));
//                 }
//                 else if(location == Location.ALL && date.Year != 0001){
//                     ProvinceReport report = await _apiService.GetNationalCovidCasesAsync(date);
//                     await RespondAsync("", embed: _embedService.EmbedCaseBuilder(report, date));
//                 }
//                 else{
//                     ProvinceReport report = await _apiService.GetProvincialCovidDataAsync(ParamterValidation.ValidateParameterInput(location.ToString().ToLower()),date);
//                     if(location == Location.PEI){
//                         if(report.data == null){
//                             await RespondAsync("PEI does not often provide updated numbers, and no data could be found for them in the last 2 days");
//                         }
//                     }
//                     await RespondAsync("", embed: _embedService.EmbedCaseBuilder(report, date));
//                 }


//             }   
//             catch(Exception ex){
//                 _log.Error( Context,ex.Message);
//             }
//         }
// }