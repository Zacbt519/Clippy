using System.Threading.Tasks;
using Discord.Interactions;

public class CatModule : InteractionModuleBase<SocketInteractionContext>
{

    public InteractionService Commands { get; set; }

    private CommandHandler _handler;

    private readonly CatAPIService _service;

    public CatModule(CommandHandler handler, CatAPIService service)
    {
        _handler = handler;
        _service = service;
    }

    [SlashCommand("cat", "Retrieve an image or gif of a cat")]
    public async Task GetCatImage()
    {
        //await this.Context.Channel.SendFileAsync(await _service.GetCatImageAsync());
        await RespondAsync(await _service.GetCatImageAsync());
    }

}