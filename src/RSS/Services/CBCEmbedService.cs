using System;
using CodeHollow.FeedReader;
using Discord;

public class CBCEmbedService
{

    private EmbedBuilder builder;

    public Embed CBCEmbedBuilder(FeedItem item)
    {
        builder = new EmbedBuilder();
        builder.WithTitle(item.Title);
        item.Description = item.Description.Trim();
        builder.WithDescription(getDescription(item.Description));
        builder.WithUrl(item.Link);
        builder.WithThumbnailUrl("https://upload.wikimedia.org/wikipedia/commons/thumb/9/99/CBC_Logo_1986-1992.svg/1024px-CBC_Logo_1986-1992.svg.png");
        builder.WithFooter(footer => footer.Text = generateFooter(item.PublishingDate));
        return builder.Build();
    }

    private string getImageURL(string desc)
    {
        string imgUrl = "";
        int endPosTag = desc.IndexOf("/>");
        string temp = desc.Substring(0, endPosTag);
        int startPos = temp.IndexOf("https");
        int endPos = temp.IndexOf(".jpg'");
        int length = endPos - startPos;
        imgUrl = temp.Substring(startPos, length);
        imgUrl = imgUrl + ".jpg";
        return imgUrl;
    }

    private string getDescription(string desc)
    {
        int startPos = desc.IndexOf("<p>");
        int endPos = desc.IndexOf("</p>");
        int length = endPos - startPos;
        string results = desc.Substring(startPos, length);
        results = results.Remove(0, 3);
        return results;
    }

    private string generateFooter(DateTime? date)
    {
        if (date == null)
        {
            return "Published date not available";
        }
        else
        {
            if (date.Value.Date == DateTime.Today.Date)
            {
                return "Published today at " + date.Value.ToShortTimeString();
            }
            else
            {
                return "Published " + date.Value.ToLongDateString();
            }
        }
    }

}