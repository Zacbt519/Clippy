// using System.Threading.Tasks;
// using Discord.Interactions;

// public class UtilityModule:  InteractionModuleBase<SocketInteractionContext>{
//     public InteractionService Commands { get; set; }

//     private CommandHandler _handler;
//     private readonly CovidEmbedService _embedService;

//     private readonly CovidWebScraperService _webScraperService; 

//     public UtilityModule(CommandHandler handler, CovidEmbedService embedService, CovidWebScraperService scraperService){
//         _handler = handler;
//         _embedService = embedService;
//         _webScraperService = scraperService;
//     }

//     [SlashCommand("links", "Retrieves a list of links relevant to The Government and Covid-19")]
//     public async Task GetLinksAsync(){
//         await RespondAsync("", embed: _embedService.EmbedLinkBuilder());
//     }

//     [SlashCommand("guide", "Retrieves the URL for the New Brunswick's Governments guide to living with Covid-19")]
//     public async Task GetGuideAsync(){
//         await Context.Channel.SendMessageAsync("https://www2.gnb.ca/content/dam/gnb/Departments/eco-bce/Promo/covid-19/guide-living-with-covid-19.pdf?fbclid=IwAR172ByHLn4pA_jL8eftlz0Ba9YneRXtfOPm_z46HFPR66sfGiPXiimRc-c");   
//     }

//     [SlashCommand("zones", "Retrieves a list of all health zones in New Brunswick")]
//     public async Task GetZonesAsync(){
//         await RespondAsync("", embed: _embedService.EmbedNBZoneBuilder());
//     }

//     [SlashCommand("variants", "Retrieves a list of Covid-19 variants of concern")]
//     public async Task GetVariantsAsync(){
//         await RespondAsync("", embed: _embedService.EmbedVariantBuilder());
//     }

//     [SlashCommand("levels", "Retrieves the document showcasing the restrictions in each level")]
//     public async Task GetLevelsAsync(){
//         await RespondAsync("https://www2.gnb.ca/content/dam/gnb/Corporate/Promo/COVID19/alertlvls/docs/Alert-Level-Guidance.pdf");
//     }
// }