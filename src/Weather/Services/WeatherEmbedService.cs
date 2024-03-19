using System;
using Discord;

public class WeatherEmbedService{

    ///<summary>
    /// Image Icon URL
    /// </summary>
    private string IMAGE_URL = "http://openweathermap.org/img/w/{0}.png";

    /// <summary>
    /// Discord.Net Embed Builder
    /// </summary>
    private EmbedBuilder builder;

    /// <summary>
    /// Creates an Embed message from the <c>WeatherResponse</c> object
    /// </summary>
    /// <param name="response"><c>WeatherResponse</c> object with data from API</param>
    /// <returns>A Discord Embed Message</returns>
    public Embed WeathedEmbedBuilder(WeatherResponse response){
        builder = new EmbedBuilder();

        builder.WithTitle("Weather for " + response.name + " - " + DateTime.Now.ToShortTimeString());
        builder.WithThumbnailUrl(string.Format(IMAGE_URL, response.weather[0].icon));

        builder.AddField("Temperature", Convert.ToInt32(response.main.temp), true);
        builder.AddField("Feels Like", Convert.ToInt32(response.main.feels_like), true);

        return builder.Build();
    }
}