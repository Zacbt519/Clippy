using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;

public class SwansonModule : InteractionModuleBase<SocketInteractionContext> {

    public InteractionService Commands { get; set; }

    private CommandHandler _handler;
    private readonly SwansonAPIService _apiService;
    private readonly SwansonEmbedService _embedService;

    public SwansonModule(CommandHandler handler, SwansonAPIService service, SwansonEmbedService embedService){
        _handler = handler;
        _apiService = service;
        _embedService = embedService;
    }


    [SlashCommand("swanson", "Gets a Ron Swanson quote")]
    public async Task GetQuoteAsync(){
        await RespondAsync(embed: _embedService.QuoteBuilder(_apiService.GetQuote().Result));
        
        //do a for loop

    }
}