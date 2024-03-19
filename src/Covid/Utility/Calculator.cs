using System;

public static class Calculator{

    public static int CalculateActiveCases(ProvinceReportData report){
        return ((report.total_cases - report.total_recoveries) - report.total_fatalities);
    }

    public static double CalculatePercentOneDose(ProvinceReportData report){
        double vaccinations = report.total_vaccinations - report.total_vaccinated ?? 0;
        double total = Convert.ToDouble((vaccinations/DataLookups.NATIONAL_POPULATION) * 100);
        return total;
    }

    public static double CalculateFullyVaccinated(ProvinceReportData report){
        double vaccinations = report.total_vaccinated ?? 0;
        double total = Convert.ToDouble((vaccinations/DataLookups.NATIONAL_POPULATION) * 100);
        return total;
    }

    public static double CalculatePercentOneDoseProvincial(ProvinceReportData report, string code){
        double vaccinations = report.total_vaccinations - report.total_vaccinated ?? 0;
        double total = Convert.ToDouble((vaccinations/DataLookups.population[code]) * 100);
        return total;
    }

    public static double CalculateFullyVaccinatedProvincial(ProvinceReportData report, string code){
        double vaccinations = report.total_vaccinated ?? 0;
        double total = Convert.ToDouble((vaccinations/DataLookups.population[code]) * 100);
        return total;
    }

    public static double CalculatePromisePercent(ProvinceReportData reportData){
        return Math.Round((Convert.ToDouble(reportData.total_vaccinations) / 6000000) * 100);
    }

    public static double CalculateNBPromisePercent(ProvinceReportData report){
        double vac = Convert.ToDouble(report.total_vaccinations);
        vac = vac - Convert.ToDouble(report.total_vaccinated);
        double pop = Convert.ToDouble(DataLookups.population["nb"]);
        double percent = vac / pop * 100;

        return Math.Round(percent, 2);
    }

    public static double CalculateNBVaccinatedPercent(ProvinceReportData report){
        return Math.Round((Convert.ToDouble(report.total_vaccinated) / Convert.ToDouble(DataLookups.population["nb"])) * 100);
    }

    public static int CalculateDosesInFreezer(ProvinceReportData report){
        return (int)(report.total_vaccines_distributed - report.total_vaccinations);
    }

    public static int CalculateRate(ProvinceReportData report, int days){
        int population = DataLookups.population["nb"];
        int vaccinations = (int)(report.total_vaccinations - report.total_vaccinated);
        int rate = (population - vaccinations) / days;
        return rate;
    }

    public static int DaysUntilCanadaDay(){
        DateTime today = DateTime.Now;
        DateTime endDate = new DateTime(DateTime.Now.Year, 7,1);
        TimeSpan t = endDate - today;
        return Convert.ToInt32(t.TotalDays);
    }

    public static int DaysUntilMidJune(){
        DateTime today = DateTime.Now;
        DateTime endDate = new DateTime(DateTime.Now.Year, 6,15);
        TimeSpan t = endDate - today;
        return Convert.ToInt32(t.TotalDays);
    }

    public static int DaysUntilMidAugust(){
        DateTime today = DateTime.Now;
        DateTime endDate = new DateTime(DateTime.Now.Year, 8,15);
        TimeSpan t = endDate - today;
        return Convert.ToInt32(t.TotalDays);
    }

    public static int DaysUntilJune7(){
        DateTime today = DateTime.Now;
        DateTime endDate = new DateTime(DateTime.Now.Year, 6,7);
        TimeSpan t = endDate - today;
        return Convert.ToInt32(t.TotalDays);
    }

    public static int DaysUntilJune12(){
        DateTime today = DateTime.Now;
        DateTime endDate = new DateTime(DateTime.Now.Year, 6,12);
        TimeSpan t = endDate - today;
        return Convert.ToInt32(t.TotalDays);
    }

    public static int DaysUntilAug2(){
        DateTime today = DateTime.Now;
        DateTime endDate = new DateTime(DateTime.Now.Year, 8,2);
        TimeSpan t = endDate - today;
        return Convert.ToInt32(t.TotalDays);
    }

    public static string CalculateFirstDosePercentage(ProvinceReport report){
        double percent = Math.Round((((double)report.data[0].total_vaccinations - (double)report.data[0].total_vaccinated) / DataLookups.population[report.province] * 100), 2);
        return percent.ToString();
    }

    public static string CalculateFullyVaxxedPercentage(ProvinceReport report){
        double percent = Math.Round((double)report.data[0].total_vaccinated / DataLookups.population[report.province] * 100, 2);
        return percent.ToString();
    }


}