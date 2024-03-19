using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;

public class CountdownModule : InteractionModuleBase<SocketInteractionContext> {

    public InteractionService Commands { get; set; }

    private CommandHandler _handler;

    private UpcomingEmbedService _embed;

    private LoggingService _log;

    private UpcomingService _service;

    private string[] commands = {"INDEX", "LIST", "NEXT"};

    /// <summary>
    /// URL for the Marvel specific JSON file
    /// </summary>
    private string MARVEL_URL = "src/Countdown/Resources/UpcomingMarvel.json";

    /// <summary>
    /// URL for the Disney Specific JSON file
    /// </summary>
    private string DISNEY_URL = "src/Countdown/Resources/UpcomingDisney.json";

    /// <summary>
    /// URL for the Star Wars specific JSON file
    /// </summary>
    private string STARWARS_URL = "src/Countdown/Resources/UpcomingStarWars.json";

    public CountdownModule(CommandHandler handler,UpcomingEmbedService service, LoggingService log, UpcomingService up){
        _handler = handler;
        _embed = service;
        _log = log;
        _service = up;
    }

    [SlashCommand("countdown", "Retrieve the next film/TV series release")]
    public async Task GetNextRelease(CountdownType type, bool getAll = false, int num = 0){
        try{
            if(getAll == false){
                if(num == 0){
                    UpcomingModel model;
                    switch(type){
                        case CountdownType.Marvel:
                            model = _service.GetNextUpcoming(MARVEL_URL);
                            if(String.IsNullOrEmpty(model.TrailerURL)){
                                await RespondAsync(embed: _embed.EmbedWithCover(model));
                            }
                            else{
                                await RespondAsync(embed: _embed.EmbedWithCoverAndTrailer(model));
                            }
                            break;
                        case CountdownType.Disney:
                            model = _service.GetNextUpcoming(DISNEY_URL);
                            if(String.IsNullOrEmpty(model.TrailerURL)){
                                await RespondAsync(embed: _embed.EmbedWithCover(model));
                            }
                            else{
                                await RespondAsync(embed: _embed.EmbedWithCoverAndTrailer(model));
                            }
                            break;
                        case CountdownType.StarWars:
                            model = _service.GetNextUpcoming(STARWARS_URL);
                            if(String.IsNullOrEmpty(model.TrailerURL)){
                                await RespondAsync(embed: _embed.EmbedWithCover(model));
                            }
                            else{
                                await RespondAsync(embed: _embed.EmbedWithCoverAndTrailer(model));
                            }
                            break;
                        case CountdownType.All:
                            List<UpcomingModel> upcoming = GetAllUpcoming();
                            UpcomingModel m = upcoming.First();
                            if(String.IsNullOrEmpty(m.TrailerURL)){
                                await RespondAsync(embed: _embed.EmbedWithCover(m));
                            }
                            else{
                                await RespondAsync(embed: _embed.EmbedWithCoverAndTrailer(m));
                            }
                            break;
                    }
                }
                else{
                    List<UpcomingModel> upcoming;
                    List<Embed> embeds = new List<Embed>(); 
                    switch(type){
                        case CountdownType.Marvel:
                            upcoming = _service.GetFromJSONFile(MARVEL_URL, num);
                            foreach(UpcomingModel model in upcoming){
                                if(String.IsNullOrEmpty(model.TrailerURL)){
                                    embeds.Add(_embed.EmbedWithCover(model));
                                }
                                else{
                                    embeds.Add(_embed.EmbedWithCoverAndTrailer(model));
                                }
                            }
                            await RespondAsync(embeds: embeds.ToArray());
                            break;
                        case CountdownType.Disney:
                            upcoming = _service.GetFromJSONFile(DISNEY_URL, num);
                            foreach(UpcomingModel model in upcoming){
                                if(String.IsNullOrEmpty(model.TrailerURL)){
                                    embeds.Add(_embed.EmbedWithCover(model));
                                }
                                else{
                                    embeds.Add(_embed.EmbedWithCoverAndTrailer(model));
                                }
                            }
                            await RespondAsync(embeds: embeds.ToArray());
                            break;
                        case CountdownType.StarWars:
                            upcoming = _service.GetFromJSONFile(STARWARS_URL, num);
                            foreach(UpcomingModel model in upcoming){
                                if(String.IsNullOrEmpty(model.TrailerURL)){
                                    embeds.Add(_embed.EmbedWithCover(model));
                                }
                                else{
                                    embeds.Add(_embed.EmbedWithCoverAndTrailer(model));
                                }
                            }
                            await RespondAsync(embeds: embeds.ToArray());
                            break;
                    }
                }
            }
            else{
                switch(type){
                    case CountdownType.Marvel:
                        await RespondAsync(embed: _embed.BuildListEmbed("Upcoming Marvel Films/TV Series", MARVEL_URL));
                        break;
                    case CountdownType.Disney:
                        await RespondAsync(embed: _embed.BuildListEmbed("Upcoming Disney Films/TV Series", DISNEY_URL));
                        break;
                    case CountdownType.StarWars:
                        await RespondAsync(embed: _embed.BuildListEmbed("Upcoming Star Wars Films/TV Series", STARWARS_URL));
                        break;
                    case CountdownType.All:
                        await RespondAsync(embed: _embed.BuildListEmbed("All Upcoming Films/TV Series", GetAllUpcoming()));
                        break;
                }
            }
        }
        catch(Exception ex){
            _log.Error( Context,ex.Message);
        }
    }

    private List<UpcomingModel> GetAllUpcoming(){
        List<UpcomingModel> upcomings = new List<UpcomingModel>();
        upcomings.AddRange(_service.GetFromJSONFile(MARVEL_URL));
        upcomings.AddRange(_service.GetFromJSONFile(STARWARS_URL));
        upcomings.AddRange(_service.GetFromJSONFile(DISNEY_URL));
        upcomings.OrderBy(x => x.Date).OrderBy(y => y.NoReleaseDate == true).ToList();
        return upcomings;
    }
}