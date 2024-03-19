using System;
using Discord;

public class XKCDEmbedService{

    /// <summary>
    /// The Discord.Net Embed builder
    /// </summary>
    private EmbedBuilder builder;

    /// <summary>
    /// Creates an Embed message for the XKCD service
    /// </summary>
    /// <param name="xkcd"><c>XKCD</c></param>
    /// <returns><c>Embed</c></returns>
    public Embed EmbedXKCDBuilder(XKCD xkcd)
    {
        builder = new EmbedBuilder();
        builder.WithTitle(xkcd.Title);
        builder.WithImageUrl(xkcd.Img);
        builder.WithDescription(xkcd.Alt + Environment.NewLine + "Day " + xkcd.Num);
        return builder.Build();
    }
}