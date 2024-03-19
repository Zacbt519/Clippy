using System;
using Discord;

public class EmbedService
{

    /// <summary>
    /// Discord.Net Embed Builder
    /// </summary>
    private EmbedBuilder builder;

    public Embed MakeWewladEmbed()
    {
        builder = new EmbedBuilder();
        builder.WithTitle(DiscordEmojis.WEWLAD);
        builder.WithAuthor(DiscordEmojis.WEWLAD);
        builder.WithDescription(DiscordEmojis.WEWLAD);
        builder.WithFooter(DiscordEmojis.WEWLAD);
        builder.AddField(DiscordEmojis.WEWLAD, DiscordEmojis.WEWLAD);
        return builder.Build();
    }

    public Embed MakeNBFireBanEmbed(Uri url)
    {
        builder = new EmbedBuilder();
        builder.WithTitle("NB Fire ban - " + DateTime.Now.ToShortDateString());
        builder.WithImageUrl(url.AbsoluteUri);
        return builder.Build();
    }
}