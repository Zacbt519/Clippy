using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

public class UpcomingService {

    /// <summary>
    /// Gets data from the JSON File.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public List<UpcomingModel> GetFromJSONFile(string path){
        List<UpcomingModel> upcoming;
        using(StreamReader reader = File.OpenText(path)){
            string json = reader.ReadToEnd();
            upcoming = JsonConvert.DeserializeObject<List<UpcomingModel>>(json);
        }

        upcoming = RemovePastFeatures(upcoming);
        return upcoming;
    }

    /// <summary>
    /// Gets the data from a JSON file and removes a number of elements based on the value passed in.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public List<UpcomingModel> GetFromJSONFile(string path, int amount){
        List<UpcomingModel> upcoming;
        using(StreamReader reader = File.OpenText(path)){
            string json = reader.ReadToEnd();
            upcoming = JsonConvert.DeserializeObject<List<UpcomingModel>>(json);
        }

        upcoming = RemovePastFeatures(upcoming);
        int amountToRemove = upcoming.Count - amount;
        upcoming.RemoveRange(amount, amountToRemove);
        return upcoming;
    }

    /// <summary>
    /// Gets the next upcoming project. GetFromJSONFile already sorts the list by release date, so we just need to grab the first element of the list.
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public UpcomingModel GetNextUpcoming(string path){
        List<UpcomingModel> upcoming = GetFromJSONFile(path);
        return upcoming[0];
    }

    /// <summary>
    /// Removes any titles in the data set where its release date we are currently passed.
    /// </summary>
    /// <param name="models"></param>
    /// <returns></returns>
    private List<UpcomingModel> RemovePastFeatures(List<UpcomingModel> models){
        DateTime compareDate = new DateTime(0001,01,01);
        models.RemoveAll(x => x.Date < DateTime.Now && x.Date != compareDate);
        models = models.OrderBy(x => x.Date).OrderBy(y => y.NoReleaseDate == true).ToList();
        //models.RemoveAll(x => x.NoReleaseDate == true);
        return models;
    }

    /// <summary>
    /// No Current plans to support record creation through Discord.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="models"></param>
    [Obsolete]
    private void SaveJSONToFile(string path, List<UpcomingModel> models){
        string json = JsonConvert.SerializeObject(models, Formatting.Indented);
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// No current plans to support record creation through Discord.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="date"></param>
    /// <param name="hasReleaseDate"></param>
    /// <param name="path"></param>
    [Obsolete]
    public void AddModel(string[] name, DateTime date, bool hasReleaseDate, string path){
        string nameString = CreateNameFromArray(name);
        UpcomingModel model = CreateModel(nameString, date, hasReleaseDate);
        List<UpcomingModel> models = GetFromJSONFile(path);
        models.Add(model);
        SaveJSONToFile(path, models);
    }

    /// <summary>
    /// Not Implemented. No plans to implement at this time.
    /// </summary>
    /// <param name="id"></param>
    [Obsolete]
    public void UpdateModel(int id){

    }

    /// <summary>
    /// Not Implemented. No plans to implement at this time.
    /// </summary>
    /// <param name="id"></param>
    [Obsolete]
    public void DeleteModel(){

    }

    /// <summary>
    /// No current plans to implement a way of adding new models through discord.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="date"></param>
    /// <param name="hasReleaseDate"></param>
    /// <returns></returns>
    [Obsolete]
    private UpcomingModel CreateModel(string title, DateTime date, bool hasReleaseDate){
        UpcomingModel model = new UpcomingModel(){
            Title = title,
            Date = date,
            NoReleaseDate = hasReleaseDate
        };

        return model;
    }

    /// <summary>
    /// To be used with the creation of new records, for which there is currently no plans.
    /// </summary>
    /// <param name="array"></param>
    /// <returns></returns>
    [Obsolete]
    private string CreateNameFromArray(string[] array){
        string name = "";
        foreach(string s in array){
            name += s + " ";
        }

        return name;
    }


}