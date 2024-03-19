using System;
using Discord;


public class LoggingMessagingService{

    private EmbedBuilder builder;


    public Embed LogDebug(string msg){
        builder = new EmbedBuilder();
        builder.WithTitle("DEBUG");
        builder.WithDescription(msg);
        builder.WithColor(Color.Green);
        return builder.Build();
    }

    public Embed Log(LogMessage log){
        builder = new EmbedBuilder();

        switch(log.Severity){
            case LogSeverity.Critical:
                builder.WithTitle($"{DiscordEmojis.Radioactive} **{log.Severity}**");
                builder.WithDescription($"{log.Exception}");
                builder.WithColor(Color.DarkRed);
                break;
            case LogSeverity.Debug:
                builder.WithTitle($"{DiscordEmojis.Spider} **{log.Severity}**");
                builder.WithDescription($"{log.Exception}");
                builder.WithColor(Color.Green);
                break;
            case LogSeverity.Error:
                builder.WithTitle($"{DiscordEmojis.Skull} **{log.Severity}**");
                builder.WithDescription($"{log.Exception}");
                builder.WithColor(Color.Red);
                break;
            case LogSeverity.Info:
                builder.WithTitle($"{DiscordEmojis.InformationSource} **{log.Severity}**");
                builder.WithDescription($"{log.Exception}");
                builder.WithColor(Color.Blue);
                break;
            case LogSeverity.Warning:
                builder.WithTitle($"{DiscordEmojis.Warning} **{log.Severity}**");
                builder.WithDescription($"{log.Exception}");
                builder.WithColor(Color.LightOrange);
                break;
        }

        return builder.Build();
    }

    public Embed LogVerbose(string msg){
        builder = new EmbedBuilder();
        builder.WithTitle("VERBOSE");
        builder.WithDescription(msg);
        builder.WithColor(Color.Blue);
        return builder.Build();
    }

    public Embed LogInfo(string msg){
        builder = new EmbedBuilder();
        builder.WithTitle("INFO");
        builder.WithDescription(msg);
        builder.WithColor(Color.DarkGrey);
        return builder.Build();
    }

    public Embed LogWarning(string msg){
        builder = new EmbedBuilder();
        builder.WithTitle("WARNING");
        builder.WithDescription(msg);
        builder.WithColor(Color.LightOrange);
        return builder.Build();
    }

    public Embed LogError(string msg){
        builder = new EmbedBuilder();
        builder.WithTitle("ERROR");
        builder.WithDescription(msg);
        builder.WithColor(Color.Red);
        return builder.Build();
    }
    public Embed LogCritical(string msg){
        builder = new EmbedBuilder();
        builder.WithTitle($"{DiscordEmojis.Radioactive}");
        builder.WithDescription(msg);
        builder.WithColor(Color.DarkRed);
        return builder.Build();
    }

    public Embed Log(string msg){
        builder = new EmbedBuilder();
        builder.WithTitle("LOG");
        builder.WithDescription(msg);
        return builder.Build();
    }





}