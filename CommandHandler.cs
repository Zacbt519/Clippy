using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class CommandHandler
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _commands;
    private readonly IServiceProvider _services;

    public CommandHandler(DiscordSocketClient client, InteractionService commands, IServiceProvider services)
    {
        _client = client;
        _commands = commands;
        _services = services;
    }

    public async Task InitializeAsync()
    {
        // Add the public modules that inherit InteractionModuleBase<T> to the InteractionService
        await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        // Process the InteractionCreated payloads to execute Interactions commands
        _client.InteractionCreated += HandleInteraction;

        _client.MessageReceived += MessageReceived;

        // Process the command execution results 
        _commands.SlashCommandExecuted += SlashCommandExecuted;
        _commands.ContextCommandExecuted += ContextCommandExecuted;
        _commands.ComponentCommandExecuted += ComponentCommandExecuted;

        await QuartzSetupAsync();
    }

    private async Task MessageReceived(SocketMessage message)
    {
        // if(message.Content.Contains("cunt", StringComparison.InvariantCultureIgnoreCase) || message.Content.Contains("cunts", StringComparison.InvariantCultureIgnoreCase)){
        //     var channel = message.Channel as SocketGuildChannel;
        //     IGuild guild = channel.Guild;
        //     var users = await guild.GetUsersAsync();
        //     List<IGuildUser> listUsers = new List<IGuildUser>();
        //     foreach(IGuildUser s in users){
        //         listUsers.Add(s);
        //     }
        //     listUsers.RemoveAll(x => x.IsBot == true);
        //     foreach(IGuildUser user in listUsers){
        //         await user.SendMessageAsync("https://i.redd.it/vbb41eeao4621.jpg");
        //     }
        // }

        // if(message.Content.Contains("beans", StringComparison.InvariantCultureIgnoreCase) || message.Content.Contains("rice", StringComparison.InvariantCultureIgnoreCase) || message.Content.Contains("jesus christ", StringComparison.InvariantCultureIgnoreCase) || message.Content.Contains("byron", StringComparison.InvariantCultureIgnoreCase)){
        //     await message.Author.SendMessageAsync("https://www.tiktok.com/@sidetalknyc/video/7034596680293027119?is_from_webapp=1&sender_device=pc&web_id6896182041973835270");
        // }

    }

    private Task ComponentCommandExecuted(ComponentCommandInfo arg1, Discord.IInteractionContext arg2, IResult arg3)
    {
        if (!arg3.IsSuccess)
        {
            switch (arg3.Error)
            {
                case InteractionCommandError.UnmetPrecondition:
                    // implement
                    break;
                case InteractionCommandError.UnknownCommand:
                    // implement
                    break;
                case InteractionCommandError.BadArgs:
                    // implement
                    break;
                case InteractionCommandError.Exception:
                    // implement
                    break;
                case InteractionCommandError.Unsuccessful:
                    // implement
                    break;
                default:
                    break;
            }
        }


        return Task.CompletedTask;
    }

    private Task ContextCommandExecuted(ContextCommandInfo arg1, Discord.IInteractionContext arg2, IResult arg3)
    {
        if (!arg3.IsSuccess)
        {
            switch (arg3.Error)
            {
                case InteractionCommandError.UnmetPrecondition:
                    // implement
                    break;
                case InteractionCommandError.UnknownCommand:
                    // implement
                    break;
                case InteractionCommandError.BadArgs:
                    // implement
                    break;
                case InteractionCommandError.Exception:
                    // implement
                    break;
                case InteractionCommandError.Unsuccessful:
                    // implement
                    break;
                default:
                    break;
            }
        }

        return Task.CompletedTask;
    }

    private Task SlashCommandExecuted(SlashCommandInfo arg1, Discord.IInteractionContext arg2, IResult arg3)
    {
        if (!arg3.IsSuccess)
        {
            switch (arg3.Error)
            {
                case InteractionCommandError.UnmetPrecondition:
                    // implement
                    break;
                case InteractionCommandError.UnknownCommand:
                    // implement
                    break;
                case InteractionCommandError.BadArgs:
                    // implement
                    break;
                case InteractionCommandError.Exception:
                    // implement
                    break;
                case InteractionCommandError.Unsuccessful:
                    // implement
                    break;
                default:
                    break;
            }
        }

        return Task.CompletedTask;
    }

    private async Task HandleInteraction(SocketInteraction arg)
    {
        try
        {
            // Create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules
            var ctx = new SocketInteractionContext(_client, arg);
            await _commands.ExecuteCommandAsync(ctx, _services);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);

            // If a Slash Command execution fails it is most likely that the original interaction acknowledgement will persist. It is a good idea to delete the original
            // response, or at least let the user know that something went wrong during the command execution.
            if (arg.Type == InteractionType.ApplicationCommand)
                await arg.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
        }
    }

    public async Task QuartzSetupAsync()
    {
        StdSchedulerFactory factory = new StdSchedulerFactory();
        IScheduler scheduler = await factory.GetScheduler();

        await scheduler.Start();

        // IJobDetail job = JobBuilder.Create<WednesdayJob>().WithIdentity("job1", "group1").Build();
        // IJobDetail job2 = JobBuilder.Create<TheWeekendJob>().WithIdentity("job2", "group1").Build();
        // IJobDetail lottoJob = JobBuilder.Create<LottoJob>().WithIdentity("job3","group1").Build();
        //IJobDetail dailyReport = JobBuilder.Create<DailyCovidReport>().WithIdentity("daily", "group1").Build();

        //IJobDetail ribJob = JobBuilder.Create<RibFestJob>().WithIdentity("job3", "group1").Build();
        //ITrigger ribTrigger = TriggerBuilder.Create().WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(12, 0, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday, DayOfWeek.Sunday).InTimeZone(TimeZoneInfo.Local)).Build();
        //await scheduler.ScheduleJob(ribJob, ribTrigger);



        //ITrigger trigger = TriggerBuilder.Create().WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(8, 30, DayOfWeek.Wednesday).InTimeZone(TimeZoneInfo.Local)).Build();
        //ITrigger trigger1 = TriggerBuilder.Create().WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(17,0,DayOfWeek.Friday).InTimeZone(TimeZoneInfo.Local)).Build();
        //ITrigger lottoTrigger = TriggerBuilder.Create().WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(7,00,DayOfWeek.Wednesday).InTimeZone(TimeZoneInfo.Local)).Build();
        //ITrigger dailyReportTrigger = TriggerBuilder.Create().WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(16,0, DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday).InTimeZone(TimeZoneInfo.Local)).Build();

        //await scheduler.ScheduleJob(job, trigger);
        //await scheduler.ScheduleJob(job2, trigger1);
        //await scheduler.ScheduleJob(lottoJob, lottoTrigger);
        //await scheduler.ScheduleJob(dailyReport, dailyReportTrigger);

        await Task.Delay(TimeSpan.FromSeconds(10));

        //await scheduler.Shutdown();
    }
}