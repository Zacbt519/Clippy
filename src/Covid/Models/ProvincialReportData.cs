using System;
public class ProvinceReportData{

    /// <summary>
    /// Date the data was grabbed? This may be obsolete.
    /// </summary>
    public DateTime date;

    /// <summary>
    /// Number of new cases
    /// </summary>
    public int? change_cases;

    /// <summary>
    /// Number of new fatalities
    /// </summary>
    public int? change_fatalities;

    /// <summary>
    /// Number of new tests conducted
    /// </summary>
    public int change_tests;

    /// <summary>
    /// Number of new hospitalizations. (Negative numbers mean someone was released from hospital)
    /// </summary>
    public int change_hospitalizations;

    /// <summary>
    /// Number of new ICU admissions. (Negative numbers mean someone left the ICU)
    /// </summary>
    public int change_criticals;

    /// <summary>
    /// Number of new recoveries
    /// </summary>
    public int change_recoveries;

    /// <summary>
    /// Number of new vaccinations
    /// </summary>
    public int? change_vaccinations;

    /// <summary>
    /// Number of new vaccines distributed
    /// </summary>
    public int? change_vaccines_distributed;

    /// <summary>
    /// Number of new people fully vaccinated
    /// </summary>
    public int? change_vaccinated;

    /// <summary>
    /// Number of total cases
    /// </summary>
    public int total_cases;

    /// <summary>
    /// Number of total fatalities
    /// </summary>
    public int total_fatalities;

    /// <summary>
    /// Number of total Tests conducted
    /// </summary>
    public int total_tests;

    /// <summary>
    /// Number of total hospitalizations
    /// </summary>
    public int total_hospitalizations;

    /// <summary>
    /// Number of total people admitted into the ICU
    /// </summary>
    public int total_criticals;

    /// <summary>
    /// Number of total recoveries
    /// </summary>
    public int total_recoveries;

    /// <summary>
    /// Number of total vaccinations
    /// </summary>
    public int? total_vaccinations;

    /// <summary>
    /// Number of total vaccinations distributed
    /// </summary>
    public int? total_vaccines_distributed;

    /// <summary>
    /// Number of total people Vaccinated
    /// </summary>
    public int? total_vaccinated;

}