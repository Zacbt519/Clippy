using System.Threading.Tasks;
using Discord.Interactions;

public class WeatherModule: InteractionModuleBase<SocketInteractionContext>{

    public InteractionService Commands { get; set; }

    private CommandHandler _handler;

    /// <summary>
    /// The Weather service
    /// </summary>
    private readonly WeatherService _service;

    /// <summary>
    /// The Weather Embed service
    /// </summary>
    private readonly WeatherEmbedService _embed;

    public WeatherModule(CommandHandler handler, WeatherService service, WeatherEmbedService embedService){
        _handler = handler;
        _service = service;
        _embed = embedService;
    }

    [SlashCommand("weather", "Get the weather for a certain location")]
    public async Task GetWeatherAsync(WeatherCities city){
        WeatherResponse w = await _service.GetWeatherAsync((int)city);
        await RespondAsync(embed: _embed.WeathedEmbedBuilder(w));
    }
}