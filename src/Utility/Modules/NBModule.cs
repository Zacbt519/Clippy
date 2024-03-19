using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;

public class NBModule : InteractionModuleBase<SocketInteractionContext>
{

    public InteractionService Commands { get; set; }

    private CommandHandler _handler;
    private NBGovService _govService;

    private readonly EmbedService _service;

    public NBModule(CommandHandler handler, EmbedService service, NBGovService govService)
    {
        _handler = handler;
        _service = service;
        _govService = govService;
    }

    [SlashCommand("fireban", "Return an image for the current firebanm rules in a specified province")]
    public async Task GetFireBanImageAsync(FirebanProvince province)
    {
        if (province == FirebanProvince.NB)
        {
            _govService.GetNBFirebanImage();
            await this.Context.Channel.SendFileAsync("src/Utility/Images/FirebanNB.gif");
        }

        if (province == FirebanProvince.NS)
        {
            _govService.GetNSFirebanImage();
            await this.Context.Channel.SendFileAsync("src/Utility/Images/FirebanNS.jpg");
        }

        // if (province === FirebanProvince.NS)
        // {
        //     _govService.GetNSFirebanImage();
        //     var
        // }
    }

    // [SlashCommand("wewlad", "wewlad")]
    // public async Task DoWewLadAsync()
    // {
    //     IGuild guild = Context.Guild;
    //     var users = await guild.GetUsersAsync();
    //     List<IGuildUser> listUsers = new List<IGuildUser>();
    //     foreach (IGuildUser s in users)
    //     {
    //         listUsers.Add(s);
    //     }
    //     listUsers.RemoveAll(x => x.IsBot == true);
    //     listUsers.Shuffle();

    //     foreach (IGuildUser user in listUsers)
    //     {
    //         await user.SendMessageAsync("", false, _service.MakeWewladEmbed());
    //     }

    //     await RespondAsync(embed: _service.MakeWewladEmbed());
    // }
}