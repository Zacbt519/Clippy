using System;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.Interactions;

public class XKCDModule : InteractionModuleBase<SocketInteractionContext>{

    public InteractionService Commands { get; set; }

    private CommandHandler _handler;

    /// <summary>
    /// The XKCD Api service
    /// </summary>
    public XKCDService _service;

    /// <summary>
    /// The XKCD Embed service
    /// </summary>
    public XKCDEmbedService _embedService;

    /// <summary>
    /// Constructor for the XKCD Command Module with Dependency Injection
    /// </summary>
    /// <param name="service">The Api serivce</param>
    /// <param name="embedService">The Embed service</param>
    public XKCDModule(CommandHandler handler,XKCDService service, XKCDEmbedService embedService){
        _handler = handler;
        _service = service;
        _embedService = embedService;
    }

    [SlashCommand("xkcd", "Retrieves an XKCD comic. Can retrieve todays, a specific number, or a random comic")]
    public async Task GetXKCD(XKCDChoices choices, int num = -1){
        if(num == -1){
            if(choices == XKCDChoices.Today){
                XKCD img = await _service.GetTodayAsync();
                await RespondAsync(embed: _embedService.EmbedXKCDBuilder(img));
            }
            else{
                XKCD img = await _service.GetRandomAsync();
                await RespondAsync(embed: _embedService.EmbedXKCDBuilder(img));
            }
        }
        else{
            XKCD img = await _service.GetNumAsync(num);
            await RespondAsync(embed: _embedService.EmbedXKCDBuilder(img));
        }
    }

    /// <summary>
    /// Gets an XKCD comic by ID
    /// </summary>
    /// <param name="num">The comic ID number</param>
    /// <returns>A Discord Embed message</returns>
    [Command]
    public async Task GetXKCD(int num){
        XKCD img = await _service.GetNumAsync(num);
        await ReplyAsync("", false, _embedService.EmbedXKCDBuilder(img));
    }

    /// <summary>
    /// Gets the XKCD comic published today
    /// </summary>
    /// <returns>A Discord Embed message</returns>
    [Command]
    public async Task GetXKCDTodayAsync(){
        XKCD img = await _service.GetTodayAsync();
        await ReplyAsync("", false, _embedService.EmbedXKCDBuilder(img));
    }

    /// <summary>
    /// Gets a random XKCD comic
    /// </summary>
    /// <returns>A Discord Embed message</returns>
    [Command("random")]
    public async Task GetXKCDRandomAsync(){
        XKCD img = await _service.GetRandomAsync();
        await ReplyAsync("", false, _embedService.EmbedXKCDBuilder(img));
    }
}