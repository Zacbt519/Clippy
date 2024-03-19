using Discord;

public class SwansonEmbedService{
    public EmbedBuilder embed;

    public Embed QuoteBuilder(string quote){
        embed = new EmbedBuilder();

        embed.WithTitle("Ron Swanson Quote");
        embed.WithDescription(quote);
        embed.WithThumbnailUrl("https://upload.wikimedia.org/wikipedia/en/thumb/a/ae/RonSwanson.jpg/250px-RonSwanson.jpg");
        return embed.Build();
    }
}