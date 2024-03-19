using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeHollow.FeedReader;
using Discord;
using Discord.Interactions;

public class CBCModule : InteractionModuleBase<SocketInteractionContext>
{

    public InteractionService Commands { get; set; }

    private CommandHandler _handler;

    private CBCRSSService _service;

    private CBCEmbedService _embed;
    public CBCModule(CommandHandler handler, CBCRSSService service, CBCEmbedService embed)
    {
        _handler = handler;
        _service = service;
        _embed = embed;
    }

    [SlashCommand("cbc", "Retrieve CBC articles")]
    public async Task GetCBCArticles(CBCOptions option)
    {
        if (option == CBCOptions.Local)
        {
            Feed feed = await _service.GetNewBrunswickFeed();
            List<Embed> embeds = new List<Embed>();
            for (int i = 0; i <= 4; i++)
            {
                Embed e = _embed.CBCEmbedBuilder(feed.Items[i]);
                embeds.Add(e);
            }


            await RespondAsync(embeds: embeds.ToArray());
        }
    }

}