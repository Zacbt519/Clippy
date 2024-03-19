using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

public class WeatherService{

    /// <summary>
    /// The ID code for Riverview, NB
    /// </summary>
    private const string RIV_CODE = "6122758";

    /// <summary>
    /// The ID code for Moncton, NB
    /// </summary>
    private const string MONCTON_CODE = "6076211";

    /// <summary>
    /// The API token
    /// </summary>
    private string TOKEN;

    /// <summary>
    /// The API URL to get weather for City by ID
    /// </summary>
    private string GET_BY_CITY_ID_URL = "http://api.openweathermap.org/data/2.5/weather?id={0}&appid={1}&units=metric";

    /// <summary>
    /// The API URL to get Icons for weather
    /// </summary>
    private string ICON_URL = "http://openweathermap.org/img/w/{0}.png";

    /// <summary>
    /// Weather Service Constructor. Initializes the API token.
    /// </summary>
    public WeatherService(){
        using(StreamReader r = File.OpenText("src/Resources/apiKeys.json")){
            string json = r.ReadToEnd();
            dynamic values = JsonConvert.DeserializeObject(json);
            TOKEN = values.weather;
        }
    }

    /// <summary>
    /// Gets the Weather for Riverview, NB
    /// </summary>
    /// <returns>A <c>WeatherResponse</c> object with data for Riverview, NB</returns>
    public async Task<WeatherResponse> GetRiverviewWeatherAsync(){
        string url = string.Format(GET_BY_CITY_ID_URL,RIV_CODE,TOKEN);
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<WeatherResponse>(response.Content);

    }

    /// <summary>
    /// Gets the Weather for given city
    /// </summary>
    /// <returns>A <c>WeatherResponse</c> object with data for Riverview, NB</returns>
    public async Task<WeatherResponse> GetWeatherAsync(int city){
        string url = string.Format(GET_BY_CITY_ID_URL,city,TOKEN);
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<WeatherResponse>(response.Content);

    }

    /// <summary>
    /// Gets the Weather for Moncton, NB
    /// </summary>
    /// <returns>A <c>WeatherResponse</c> object with data for Moncton, NB</returns>
    public async Task<WeatherResponse> GetMonctonWeatherAsync(){
        string url = string.Format(GET_BY_CITY_ID_URL,MONCTON_CODE,TOKEN);
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        return JsonConvert.DeserializeObject<WeatherResponse>(response.Content);

    }
}