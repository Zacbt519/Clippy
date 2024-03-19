using System;
using System.Collections.Generic;
using Discord;

public class UpcomingEmbedService{

    /// <summary>
    /// Data Retrieval Service
    /// </summary>
    private UpcomingService _service;

    /// <summary>
    /// Discord Embed Builder
    /// </summary>
    private EmbedBuilder builder;

    /// <summary>
    /// Constructor with Dependency Injection
    /// </summary>
    /// <param name="service"></param>
    public UpcomingEmbedService(UpcomingService service){
        _service = service;
    }

    /// <summary>
    /// Creates an Embed Message of a list of upcoming titles based on a specific dataset.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="pathToJson"></param>
    /// <returns></returns>
    public Embed BuildListEmbed(string title, string pathToJson){
        builder = new EmbedBuilder();
        string t = "";

        List<UpcomingModel> upcoming = _service.GetFromJSONFile(pathToJson);
        builder.WithTitle(title);

        foreach(UpcomingModel m in upcoming){
            if(m.NoReleaseDate == false){
                t+= "--------------------" + Environment.NewLine + m.Title + Environment.NewLine + DaysUntil(m.Date) + " days" + Environment.NewLine;
              
            }
            else{
                    t+= "--------------------" + Environment.NewLine + m.Title + Environment.NewLine + "Release Date TBD" + Environment.NewLine;
                
            }
            
        }

        builder.WithDescription(t);
        return builder.Build();

    }

    public Embed BuildListEmbed(string title, List<UpcomingModel> models){
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates an Embed messages based on the dataset passed. The message is for seeing the index position of each title. This command is not really useful at this point in time.
    /// </summary>
    /// <param name="pathToJson"></param>
    /// <returns></returns>
    public Embed BuildIndexEmbed(string pathToJson){
        builder = new EmbedBuilder();
        List<UpcomingModel> upcoming = _service.GetFromJSONFile(pathToJson);
        builder.WithTitle("Index");

        for(int i = 0; i < upcoming.Count; i++){
            builder.AddField(upcoming[i].Title, "Index Num [" + i + "]");
        }

        return builder.Build();
    }

    /// <summary>
    /// Builds an Embed Message for the next upcoming title.
    /// </summary>
    /// <param name="pathToJson"></param>
    /// <returns></returns>
    public Embed BuildNextEmbed(string pathToJson){
        builder = new EmbedBuilder();
        List<UpcomingModel> upcoming = _service.GetFromJSONFile(pathToJson);

        builder.WithTitle("Next upcoming Movie/TV series");

        builder.AddField(upcoming[0].Title, DaysUntil(upcoming[0].Date) + " days");
        return builder.Build();
    }

    /// <summary>
    /// Builds an Embed message to display information about the title that is passed in to the function
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Embed EmbedWithCover(UpcomingModel model){
        builder = new EmbedBuilder();

        if(model.Type == ModelType.Movie){
            builder.WithTitle(model.Title);
            builder.WithThumbnailUrl(model.CoverURL);
            builder.WithDescription(model.Description);
            if(!string.IsNullOrWhiteSpace(model.Director)){
                builder.AddField("Directed by: ", model.Director,true);
            }
            if(model.Duration != 0 ){
                builder.AddField("Runtime: ", model.Duration + " minutes", true);
            }
            if(model.Phase != 0){
                builder.AddField("Phase: ", model.Phase, true);
            }
            builder.AddField("Release Date: ", model.Date.ToShortDateString(), true);
            builder.AddField("Number of days til release: ", DaysUntil(model.Date) + " Days", true);
        }
        else{
            builder.WithTitle(model.Title);
            builder.WithThumbnailUrl(model.CoverURL);
            builder.WithDescription(model.Description);
            if(!string.IsNullOrWhiteSpace(model.Director)){
                builder.AddField("Showrunner: ", model.Director,true);
            }
            if(model.NumberOfSeasons != 0){
                builder.AddField("Number of Seasons: ", model.NumberOfSeasons, true);
            }
            if(model.NumberOfEpisodes != 0){
                builder.AddField("Number of Episodes: ", model.NumberOfEpisodes, true);
            }
            builder.AddField("Release Date: ", model.Date.ToShortDateString(), true);
            if(model.Phase != 0){
                builder.AddField("Phase: ", model.Phase, true);
            }
            builder.AddField("Number of days til release: ", DaysUntil(model.Date) + " Days", true);
        }

        return builder.Build();
        
    }

    /// <summary>
    /// Builds an Embed message to display information about the title (that has a cover image and/or trailer) that is passed in to the function
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Embed EmbedWithCoverAndTrailer(UpcomingModel model){
        builder = new EmbedBuilder();
        if(model.Type == ModelType.Movie){
            builder.WithTitle(model.Title);
            builder.WithThumbnailUrl(model.CoverURL);
            builder.WithDescription(model.Description);
            if(!string.IsNullOrWhiteSpace(model.Director)){
                builder.AddField("Directed by: ", model.Director,true);
            }
            if(model.Duration != 0 ){
                builder.AddField("Runtime: ", model.Duration + " minutes", true);
            }
            if(model.Phase != 0){
                builder.AddField("Phase: ", model.Phase, true);
            }
            builder.AddField("Release Date: ", model.Date.ToShortDateString(), true);
            builder.AddField("Number of days til release: ", DaysUntil(model.Date) + " Days", true);
            builder.Url = model.TrailerURL;
        }
        else{
            builder.WithTitle(model.Title);
            builder.WithThumbnailUrl(model.CoverURL);
            builder.WithDescription(model.Description);
            if(!string.IsNullOrWhiteSpace(model.Director)){
                builder.AddField("Showrunner: ", model.Director,true);
            }
            if(model.NumberOfSeasons != 0){
                builder.AddField("Number of Seasons: ", model.NumberOfSeasons, true);
            }
            if(model.NumberOfEpisodes != 0){
                builder.AddField("Number of Episodes: ", model.NumberOfEpisodes, true);
            }
            builder.AddField("Release Date: ", model.Date.ToShortDateString(), true);
            if(model.Phase != 0){
                builder.AddField("Phase: ", model.Phase, true);
            }
            builder.AddField("Number of days til release: ", DaysUntil(model.Date) + " Days", true);
            builder.Url = model.TrailerURL;
        }

        return builder.Build();
    }

    private int DaysUntil(DateTime date){
        return Convert.ToInt32((date - DateTime.Now).TotalDays);
    }
}