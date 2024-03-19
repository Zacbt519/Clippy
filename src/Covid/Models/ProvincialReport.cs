using System;
using System.Collections.Generic;
public class ProvinceReport{

    /// <summary>
    /// Province Code
    /// </summary>
    public string province;

    /// <summary>
    /// List of the reported data
    /// </summary>
    public List<ProvinceReportData> data;

    /// <summary>
    /// Date the request was made
    /// </summary>
    public DateTime date;

    /// <summary>
    /// Date the data was last updated in the API
    /// </summary>
    public DateTime last_updated;
}