using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class MicrosecondEpochConverter : DateTimeConverterBase
{
    /// <summary>
    /// Point in time where Unix Timestamps started
    /// </summary>
    /// <returns></returns>
    public static readonly DateTime _epoch = new DateTime(1970,1,1,0,0,0,DateTimeKind.Utc);

    /// <summary>
    /// Reads in a JSON Value
    /// </summary>
    /// <param name="reader">JSON Reader</param>
    /// <param name="objectType"></param>
    /// <param name="existingValue"></param>
    /// <param name="serializer"></param>
    /// <returns></returns>
    public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.Value == null) { return null; }
        return _epoch.AddMilliseconds((long)reader.Value / 1000d);
    }

    /// <summary>
    /// Writes out a JSON value
    /// </summary>
    /// <param name="writer">JSON Writer</param>
    /// <param name="value"></param>
    /// <param name="serializer"></param>
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        writer.WriteRawValue(((DateTime)value - _epoch).TotalMilliseconds + "000");
    }
}
