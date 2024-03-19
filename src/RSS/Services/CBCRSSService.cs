using System.Xml;
using System.ServiceModel;
using System.Threading.Tasks;
using CodeHollow.FeedReader;

public class CBCRSSService
{
    private string LOCAL_URL = "https://www.cbc.ca/cmlink/rss-canada-newbrunswick";

    public async Task<Feed> GetNewBrunswickFeed()
    {
        Feed feed = await FeedReader.ReadAsync(LOCAL_URL);
        return feed;
    }
}