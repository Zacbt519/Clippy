using System.Threading.Tasks;
using RestSharp;

public class SwansonAPIService{

    private const string URL = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";

    public async Task<string> GetQuote(){
        var client = new RestClient(URL);
        var request = new RestRequest(URL, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        return FormatString(response.Content);
    }

    private string FormatString(string quote){
        int length = quote.Length;
        quote = quote.Substring(2, length-4);
        return quote;
    }
}