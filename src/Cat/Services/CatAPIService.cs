using System;
using System.IO;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json;
using RestSharp;
using TT6Exceptions;

public class CatAPIService{

    public CatAPIService(){
        using(StreamReader r = File.OpenText("src/Resources/apiKeys.json")){
            string json = r.ReadToEnd();
            dynamic values = JsonConvert.DeserializeObject(json);
            _apiKey = values.cat;
        }
    }

    private readonly string _apiKey;

    private static string BASE_URL = "https://cataas.com/cat";

    private static string CAT_API = "https://api.thecatapi.com/v1/images/search";

    private static string TAG_URL = BASE_URL + "/{0}";

    private static string GIF_URL = BASE_URL + "/gif";


    
    public async Task<string> GetCatImageAsync(){
        var client = new RestClient(CAT_API);
        var request = new RestRequest(CAT_API, DataFormat.Json);
        request.AddHeader("x-api-key", _apiKey);

        var response = await client.ExecuteAsync(request);
        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }
        dynamic obj = JsonConvert.DeserializeObject(response.Content);
        Console.Write("here");

        return obj[0].url;
    }
}