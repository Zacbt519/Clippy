using System;

public class UpcomingModel {

    /// <summary>
    /// Title of upcoming Movie/TV Show
    /// </summary>
    public string Title;

    /// <summary>
    /// Release Date
    /// </summary>
    public DateTime Date;

    /// <summary>
    /// Type of Upcoming
    /// </summary>
    public ModelType Type;

    /// <summary>
    /// Flag for if the project has a release date
    /// </summary>
    public bool NoReleaseDate;

    /// <summary>
    /// Project Description
    /// </summary>
    public string Description;

    /// <summary>
    /// Poster Art URL
    /// </summary>
    public string CoverURL;

    /// <summary>
    /// URL to the trailer
    /// </summary>
    public string TrailerURL;

    /// <summary>
    /// Director/Showrunner for the project
    /// </summary>
    public string Director;

    /// <summary>
    /// Duration (Only Applicaple for Type.Movies)
    /// </summary>
    public int Duration;

    /// <summary>
    /// Specific to Marvel Projects. What MCU phase the project is in
    /// </summary>
    public int Phase;

    /// <summary>
    /// Number of Episodes
    /// </summary>
    public int NumberOfEpisodes;

    /// <summary>
    /// Number of Seasons
    /// </summary>
    public int NumberOfSeasons;

}

public enum ModelType{
    Movie,
    TV
}