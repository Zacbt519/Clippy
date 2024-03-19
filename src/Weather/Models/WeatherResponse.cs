using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class WeatherResponse{

    /// <summary>
    /// The coordinates
    /// </summary>
    public Coords coord;

    /// <summary>
    /// The Weather type
    /// </summary>
    public List<WeatherType> weather;

    /// <summary>
    /// The Weather
    /// </summary>
    public Weather main;

    /// <summary>
    /// The visibility
    /// </summary>
    public int visibility;

    /// <summary>
    /// The Wind
    /// </summary>
    public Wind wind;

    /// <summary>
    /// The Clouds
    /// </summary>
    public Clouds clouds;

    /// <summary>
    /// The date
    /// </summary>
    [JsonConverter(typeof(MicrosecondEpochConverter))]
    public DateTime dt;

    public string name;
}

    



