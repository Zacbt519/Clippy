using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TT6Exceptions;

public class CovidWebScraperService {

    private string EXPOSURES_URL = "https://www2.gnb.ca/content/gnb/en/corporate/promo/covid-19/potential_public_exposure.html";
    private string ZONE_ONE_XPATH = "/html/body/div/article/div/div[9]/div/div[1]/div/div[3]/div/ul/li";
    private string UPDATE_XPATH = "/html/body/div/article/div/div[9]/div/div[1]/div/div[2]/div/p/i";

    private string NEW_XPATH = "//*[@id=\"container\"]/article/div/div[7]/div/div[1]/div/div[3]/div/ul/li";

    private string PAST_XPATH = "//*[@id=\"collapse_0_zone1\"]/div/div/div/ul/li";
  
    private string DATE_XPATH = "//*[@id=\"container\"]/article/div/div[5]/div/p/b";

//     public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)       
//    => self.Select((item, index) => (item, index)); 

    /// <summary>
    /// Gets a <c>List</c> of <c>List</c> of <c>string</c> with all the exposures in Moncton asynchronously
    /// </summary>
    /// <returns>A <c>List</c> of <c>List</c> of <c>string</c> of all exposures in Moncton</returns>
    public async Task<List<List<string>>> GetPublicExposuresAsync(){
        var web = new HtmlWeb();
        web.OverrideEncoding = System.Text.Encoding.Unicode;
        HtmlDocument doc = await web.LoadFromWebAsync(EXPOSURES_URL);
        var nodes = doc.DocumentNode.SelectNodes(ZONE_ONE_XPATH);
        if(nodes == null){
            throw new ApiException("Web data not found.");
        }
        List<string> exposures = new List<string>();
        foreach(HtmlNode li in nodes){
            exposures.Add(li.InnerText);
        }

        List<List<string>> list = new List<List<string>>();
        string message = String.Join(" ", exposures);

        if(message.Length > 4096){
            double amount = (double)(Convert.ToDouble(message.Length) / 4096.0);
            decimal numEmbeds = (decimal)Math.Ceiling(amount);

                for(int i = 0; i <= numEmbeds; i++){
                    int msgLength = 0;
                    int lengthCounter = 0;
                    List<string> myList = new List<string>();
                    for(int j = 0; j < exposures.Count; j++){
                        if(msgLength <= 4000){
                            string msg = exposures[j].Replace("&nbsp;"," ");
                            msg = msg.Replace("&amp;", "&");
                            int temp = msg.Length;
                            msgLength += temp;

                            if(msgLength > 4000){
                                msgLength -= temp;
                                break;
                            }
                            else{
                                myList.Add(msg);
                                msgLength += msg.Length;
                                lengthCounter++;
                            }    
                        }
                    }
                exposures.RemoveRange(0,lengthCounter);
                list.Add(myList);
            }
        }
        return list;
    }

    public async Task<List<List<string>>> GetPastExposuresAsync(){
        var web = new HtmlWeb();
        HtmlDocument doc = await web.LoadFromWebAsync(EXPOSURES_URL);
        var nodes = doc.DocumentNode.SelectNodes(PAST_XPATH);
        if(nodes == null){
            throw new ApiException("Web data not found.");
        }
        List<string> exposures = new List<string>();
        foreach(HtmlNode li in nodes){
            exposures.Add(li.InnerText);
        }

        List<List<string>> list = new List<List<string>>();
        string message = String.Join(" ", exposures);

        if(message.Length > 4000){
            double amount = (double)(Convert.ToDouble(message.Length) / 4000.0);
            decimal numEmbeds = (decimal)Math.Ceiling(amount);

                for(int i = 0; i <= numEmbeds; i++){
                    int msgLength = 0;
                    int lengthCounter = 0;
                    List<string> myList = new List<string>();
                    for(int j = 0; j < exposures.Count; j++){
                        if(msgLength <= 4000){
                            string msg = exposures[j].Replace("&nbsp;"," ");
                            msg = msg.Replace("&amp;", "&");
                            msg = msg.Replace("-&nbsp;", "- ");
                            int temp = msg.Length;
                            msgLength += temp;

                            if(msgLength > 4000){
                                msgLength -= temp;
                                break;
                            }
                            else{
                                myList.Add(msg);
                                msgLength += msg.Length;
                                lengthCounter++;
                            }    
                        }
                    }
                exposures.RemoveRange(0,lengthCounter);
                list.Add(myList);
            }
        }
        else{
            list.Add(exposures);
        }
        return list;
    }

    public async Task<List<List<string>>> GetNewestExposuresAsync(){
        var web = new HtmlWeb();
        HtmlDocument doc = await web.LoadFromWebAsync(EXPOSURES_URL);
        var nodes = doc.DocumentNode.SelectNodes(NEW_XPATH);
        if(nodes == null){
            throw new ApiException("Web data not found.");
        }
        List<string> exposures = new List<string>();
        foreach(HtmlNode li in nodes){
            exposures.Add(li.InnerText);
        }

        List<List<string>> list = new List<List<string>>();
        string message = String.Join(" ", exposures);

        if(message.Length > 4096){
            double amount = (double)(Convert.ToDouble(message.Length) / 4000.0);
            decimal numEmbeds = (decimal)Math.Ceiling(amount);

                for(int i = 0; i <= numEmbeds; i++){
                    int msgLength = 0;
                    int lengthCounter = 0;
                    List<string> myList = new List<string>();
                    for(int j = 0; j < exposures.Count; j++){
                        if(msgLength <= 4000){
                            string msg = exposures[j].Replace("&nbsp;"," ");
                            msg = msg.Replace("&amp;", "&");
                            msg = msg.Replace("-&nbsp;", "- ");
                            int temp = msg.Length;
                            msgLength += temp;

                            if(msgLength > 4000){
                                msgLength -= temp;
                                break;
                            }
                            else{
                                myList.Add(msg);
                                msgLength += msg.Length;
                                lengthCounter++;
                            }    
                        }
                    }
                exposures.RemoveRange(0,lengthCounter);
                list.Add(myList);
            }
        }
        else{
            List<string> updatedStrings = new List<string>();
            foreach(string s in exposures){
                string msg = s;
                msg = msg.Replace("&nbsp;"," ");
                msg = msg.Replace("&amp;", "&");
                msg = msg.Replace("-&nbsp;", "- ");
                updatedStrings.Add(msg);
            }
            list.Add(updatedStrings);
        }
        return list;
    }

    /// <summary>
    /// Gets a <c>List</c> of <c>List</c> of <c>string</c> with all the new exposures in Moncton asynchronously
    /// </summary>
    /// <returns>A <c>List</c> of <c>List</c> of <c>string</c> of all the new exposures in Moncton</returns>
    public async Task<List<List<string>>> GetNewExposuresAsync(){
        var web = new HtmlWeb();
        HtmlDocument doc = await web.LoadFromWebAsync(EXPOSURES_URL);
        var nodes = doc.DocumentNode.SelectNodes(ZONE_ONE_XPATH);
        if(nodes == null){
            throw new ApiException("Web data not found.");
        }
        List<string> exposures = new List<string>();
        foreach(HtmlNode li in nodes){
            if(li.InnerText.Contains("NEW", StringComparison.InvariantCulture)){
                string msg = li.InnerText.Replace("&nbsp;", " ");
                msg = msg.Replace("&amp;", "&");
                exposures.Add(msg);
            }
            
        }

        List<List<string>> list = new List<List<string>>();
        string message = String.Join(" ", exposures);

        if(message.Length > 4096){
            double amount = (double)(Convert.ToDouble(message.Length) / 4000.0);
            decimal numEmbeds = (decimal)Math.Ceiling(amount);

                for(int i = 0; i <= numEmbeds; i++){
                    int msgLength = 0;
                    int lengthCounter = 0;
                    List<string> myList = new List<string>();
                    for(int j = 0; j < exposures.Count; j++){
                        if(msgLength <= 4000){
                            string msg = exposures[j].Replace("&nbsp;"," ");
                            int temp = msg.Length;
                            msgLength += temp;

                            if(msgLength > 4000){
                                msgLength -= temp;
                                break;
                            }
                            else{
                                myList.Add(msg);
                                msgLength += msg.Length;
                                lengthCounter++;
                            }    
                        }
                    }

                exposures.RemoveRange(0,lengthCounter);
                list.Add(myList);
            }
        }
        else{
            list.Add(exposures);
        }
        return list;
    }

    /// <summary>
    /// Gets a the date the list of exposures was last updated
    /// </summary>
    /// <returns><c>string</c> with the date last updated</returns>
    public async Task<string> GetDateAsync(){
        var web = new HtmlWeb();
        HtmlDocument doc = await web.LoadFromWebAsync(EXPOSURES_URL);
        var nodes = doc.DocumentNode.SelectNodes(DATE_XPATH);
        if(nodes == null){
            ApiException exception = new ApiException("Web data not found.");
        }
        string date = nodes[0].InnerText;
        date = date.Replace("&nbsp;",string.Empty);
        return date;
    }
}