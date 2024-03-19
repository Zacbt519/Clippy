using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;

public class DMModule: InteractionModuleBase<SocketInteractionContext> {

    public InteractionService Commands { get; set; }

    private CommandHandler _handler;

    private readonly EmbedService _service;

    public DMModule(CommandHandler handler, EmbedService service){
        _handler = handler;
        _service = service;
    }

    [SlashCommand("wewlad", "wewlad")]
    public async Task DoWewLadAsync(){
        IGuild guild = Context.Guild;
        var users = await guild.GetUsersAsync();
        List<IGuildUser> listUsers = new List<IGuildUser>();
        foreach (IGuildUser s in users){
            listUsers.Add(s);
        }
        listUsers.RemoveAll(x => x.IsBot == true);
        listUsers.Shuffle();

        foreach(IGuildUser user in listUsers){
            await user.SendMessageAsync("", false, _service.MakeWewladEmbed());
        }

        await RespondAsync(embed: _service.MakeWewladEmbed());
    }
}