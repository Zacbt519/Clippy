using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TT6Exceptions;

public class XKCDService{

    /// <summary>
    /// The XKCD API URL
    /// </summary>
     private string BASE_URL = "http://xkcd.com";

    /// <summary>
    /// The XKCD API URL for the latest comic
    /// </summary>
    private string TODAY_URL = "http://xkcd.com/info.0.json";

    /// <summary>
    /// Fetches the number of XKCD comics from the API
    /// </summary>
    /// <returns>The number of XKCD comics</returns>
    private async Task<int> GetNumFromAPIAsync(){
        var client = new RestClient(TODAY_URL);
        var request = new RestRequest(TODAY_URL, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }
        XKCD xkcd = JsonConvert.DeserializeObject<XKCD>(response.Content);
        return xkcd.Num;
    }

    /// <summary>
    /// Fetches the latest XKCD comic from the API
    /// </summary>
    /// <returns>The latest XKCD comic</returns>
    public async Task<XKCD> GetTodayAsync(){
        var client = new RestClient(TODAY_URL);
        var request = new RestRequest(TODAY_URL, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }
        XKCD xkcd = JsonConvert.DeserializeObject<XKCD>(response.Content);
        return xkcd;
    }

    /// <summary>
    /// Fetches an XKCD comic based on its ID number
    /// </summary>
    /// <param name="num">The ID number for the XKCD comic</param>
    /// <returns>An XKCD comic</returns>
    public async Task<XKCD> GetNumAsync(int num){
        string url = BASE_URL + "/" + num + "/info.0.json";
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }
        XKCD xkcd = JsonConvert.DeserializeObject<XKCD>(response.Content);
        return xkcd;
    }

    /// <summary>
    /// Fetches a random XKCD comic from the API
    /// </summary>
    /// <returns>An XKCD comic</returns>
    public async Task<XKCD> GetRandomAsync(){
        Random rnd = new Random();
        int num = rnd.Next(0,GetNumFromAPIAsync().Result + 1);
        string url = BASE_URL + "/" + num + "/info.0.json";
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }
        XKCD xkcd = JsonConvert.DeserializeObject<XKCD>(response.Content);
        return xkcd;
    }

}