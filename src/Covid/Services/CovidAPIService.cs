using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using TT6Exceptions;

public class CovidAPIService{

    private string URL_PROVINCIAL_DATA = "https://api.covid19tracker.ca/reports/province/";
    private string URL_VACCINATION_DATA = "https://api.covid19tracker.ca/reports";

    private string URL_ZONE_DATA = "https://api.opencovid.ca/summary?loc=";

    public async Task<ProvinceReport> GetProvincialCovidDataAsync(string province, DateTime date){
        string url = URL_PROVINCIAL_DATA + province + "?date=" + date.ToShortDateString().ToString();
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);

        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }

        ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
        if(report.data.Count == 0){
            date = date.AddDays(-1);
            url = URL_PROVINCIAL_DATA + province + "?date=" + date.ToShortDateString().ToString();
            client = new RestClient(url);
            request = new RestRequest(url, DataFormat.Json);
            response = await client.ExecuteAsync(request);
            report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
        }
        return report;
    }

    public async Task<ProvinceReport> GetNationalCovidCasesAsync(DateTime date){
        string url = URL_VACCINATION_DATA + "?date=" + date.ToShortDateString();
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);

        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }

        ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
        if(report.data.Count == 0){
            date = date.AddDays(-1);
            url = URL_VACCINATION_DATA + "?date=" + date.ToShortDateString().ToString();
            client = new RestClient(url);
            request = new RestRequest(url, DataFormat.Json);
            response = await client.ExecuteAsync(request);
            report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);

        }

        return report;
    }

    public async Task<ProvinceReport> GetNationalVaccinationDataAsync(){
        string url = URL_VACCINATION_DATA + "?date=" + DateTime.Now.ToShortDateString();
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);

        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }

        ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
        if(report.data.Count == 0 || report.data == null){
            url = URL_VACCINATION_DATA + "/?date=" + DateTime.Now.AddDays(-1).ToShortDateString();
            client = new RestClient(url);
            request = new RestRequest(url, DataFormat.Json);
            response = await client.ExecuteAsync(request);
            report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
            return report;
        }
        else{
            return report;
        }
    }

    public async Task<ProvinceReport> GetNationalVaccinationDataAsync(DateTime date){
        string url = URL_VACCINATION_DATA + "?date=" + date.ToShortDateString();
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);

        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }

        ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
        if(report.data.Count == 0 || report.data == null){
            date = date.AddDays(-1);
            url = URL_VACCINATION_DATA + "?date=" + date.ToShortDateString();
            client = new RestClient(url);
            request = new RestRequest(url, DataFormat.Json);
            response = await client.ExecuteAsync(request);
            report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
            return report;
        }
        else{

            return report;
        }
    }
    public async Task<ProvinceReport> GetProvincialVaccinationDataAsync(string province){
        string url = URL_VACCINATION_DATA + "/province/" + province + "?date=" + DateTime.Now.ToShortDateString();
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);

        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }

        ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
        if(report.data.Count == 0 || report.data == null){
            url = URL_VACCINATION_DATA + "/province/" + province + "?date=" + DateTime.Now.AddDays(-1).ToShortDateString();
            client = new RestClient(url);
            request = new RestRequest(url, DataFormat.Json);
            response = await client.ExecuteAsync(request);
            report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
            return report;
        }
        else{

            return report;
        }
    }

    public async Task<ProvinceReport> GetProvincialVaccinationDataAsync(string province, DateTime date){
        string url = URL_VACCINATION_DATA + "/province/" + province + "?date=" + date.ToShortDateString();
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);

        if(!response.IsSuccessful){
            throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
        }

        ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
        if(report.data.Count == 0 || report.data == null){
            date = date.AddDays(-1);
            url = URL_VACCINATION_DATA + "/province/" + province + "?date=" + date.ToShortDateString();
            client = new RestClient(url);
            request = new RestRequest(url, DataFormat.Json);
            response = await client.ExecuteAsync(request);
            report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
            return report;
        }
        else{
            return report;
        }
    }

    public async Task<List<ProvinceReport>> GetNationalDataForPastWeekAsync(DateTime startDate){
        List<ProvinceReport> reports = new List<ProvinceReport>();
        for(int i = 0; i<=6; i++){
            string url = URL_VACCINATION_DATA + "?date=" + startDate.ToShortDateString();
            var client = new RestClient(url);
            var request = new RestRequest(url, DataFormat.Json);
            var response = await client.ExecuteAsync(request);

            if(!response.IsSuccessful){
                throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
            }

            ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
            reports.Add(report);
            startDate = startDate.AddDays(-1);
        }

        return reports;
    }

    public async Task<List<ProvinceReport>> GetProvincialDataForPastWeekAsync(DateTime startDate, string province){
        List<ProvinceReport> reports = new List<ProvinceReport>();
        for(int i = 0; i<=6; i++){
            string url = URL_VACCINATION_DATA + "/province/" + province + "?date=" + startDate.ToShortDateString();
            var client = new RestClient(url);
            var request = new RestRequest(url, DataFormat.Json);
            var response = await client.ExecuteAsync(request);

            if(!response.IsSuccessful){
                throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
            }

            ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
            reports.Add(report);
            startDate = startDate.AddDays(-1);
        }

        return reports;
    }

    public async Task<List<ProvinceReport>> GetDataForAll(){

        List<ProvinceReport> reports = new List<ProvinceReport>();
        foreach(KeyValuePair<string,string> pair in DataLookups.provinces){
            if(pair.Key != "All"){
                string url = URL_VACCINATION_DATA + "/province/" + pair.Key + "?date=" + DateTime.Now.ToShortDateString();
                var client = new RestClient(url);
                var request = new RestRequest(url, DataFormat.Json);
                var response = await client.ExecuteAsync(request);

                if(!response.IsSuccessful){
                    throw new ApiException($"API Request Failed! Status code {response.StatusCode}", response.ErrorException);
                }

                ProvinceReport report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
                if(report.data.Count == 0 || report.data == null){
                    url = URL_VACCINATION_DATA + "/province/" + pair.Key + "?date=" + DateTime.Now.AddDays(-1).ToShortDateString();
                    client = new RestClient(url);
                    request = new RestRequest(url, DataFormat.Json);
                    response = await client.ExecuteAsync(request);
                    report = JsonConvert.DeserializeObject<ProvinceReport>(response.Content);
                    reports.Add(report);
                }
                else{
                    reports.Add(report);
                }
            }
        }
        return reports;
    }

    public async Task<int> GetZoneBreakdown(int loc){
        string url = URL_ZONE_DATA + loc;
        var client = new RestClient(url);
        var request = new RestRequest(url, DataFormat.Json);
        var response = await client.ExecuteAsync(request);
        Root r = JsonConvert.DeserializeObject<Root>(response.Content);
        return Convert.ToInt32(r.summary[0].cases);
    }
}