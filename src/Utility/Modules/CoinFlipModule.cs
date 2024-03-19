using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Interactions;

public class CoinFlipModule: InteractionModuleBase<SocketInteractionContext> {
    public InteractionService Commands { get; set; }

    private CommandHandler _handler;

    private static Random rng = new Random();

    public CoinFlipModule(CommandHandler handler){
        _handler = handler;
    }

    [SlashCommand("flip", "Flip a coin")]
    public async Task DoCoinFlipAsync(){
        int val = rng.Next(0,6001);
        Console.WriteLine(val);

        if(val >= 0 && val <= 2999){
            await RespondAsync("Winner: Heads");
        }
        else if( val >= 3000 && val <= 5999){
            await RespondAsync("Winner: Tails");
        }
        else if ( val == 6000){
            await RespondAsync("The coin landed on the edge....... how lucky!");
        }
    }

    [SlashCommand("pick", "Pick a random user in the server who is not a bot")]
    public async Task PickUserInServer(){
        IGuild guild = Context.Guild;
        var users = await guild.GetUsersAsync();
        List<IGuildUser> listUsers = new List<IGuildUser>();
        foreach (IGuildUser s in users){
            listUsers.Add(s);
        }
        listUsers.RemoveAll(x => x.IsBot == true);
        listUsers.Shuffle();
        await RespondAsync(listUsers[0].Mention + " was picked");
    }

}