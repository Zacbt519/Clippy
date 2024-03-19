using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Discord;
public class CovidEmbedService{

    private EmbedBuilder builder;

    /// <summary> 
    /// Builds an Embed message with Provincial/Federal Case Data
    /// </summary>
    ///<param name="report">A <c>ProvinceReport</c> object</param>
    ///<param name="date"> A <c>DateTime</c> object</param>
    ///<returns><c>Embed</c></returns>
    public Embed EmbedCaseBuilder(ProvinceReport report, DateTime date){
        builder = new EmbedBuilder();
        builder.WithThumbnailUrl(DataLookups.flags[report.province]);

        if(date.Date == report.last_updated.Date){
            builder.WithTitle(DataLookups.provinces[report.province] + " Covid Update - " + report.last_updated.ToShortDateString() + " - " + report.last_updated.ToShortTimeString());
        }
        else{
            builder.WithTitle(DataLookups.provinces[report.province] + " Covid Update - " + date.ToShortDateString() + " - " + date.ToShortTimeString());

        }

        builder.AddField("New Cases", report.data[0].change_cases.ToString() ?? "0");
        builder.AddField("New Deaths", report.data[0].change_fatalities.ToString() ?? "0");
        builder.AddField("New Hospitalizations", report.data[0].change_hospitalizations.ToString() ?? "0");
        builder.AddField("New ICU Cases", report.data[0].change_criticals.ToString() ?? "0");
        builder.AddField("New Recoveries", report.data[0].change_recoveries.ToString() ?? "0");
        builder.AddField("Total Cases", report.data[0].total_cases.ToString() ?? "0");
        builder.AddField("Total Deaths", report.data[0].total_fatalities.ToString() ?? "0");
        builder.AddField("Active Hospitalizations", report.data[0].total_hospitalizations.ToString() ?? "0");
        builder.AddField("Active ICU Cases", report.data[0].total_criticals.ToString() ?? "0");
        builder.AddField("Total Recoveries", report.data[0].total_recoveries.ToString() ?? "0");
        builder.AddField("Total Active Cases", Calculator.CalculateActiveCases(report.data[0]));

        builder.WithColor(Color.DarkTeal);

        if(report.province.Equals("on")){
            builder.WithFooter("* Ontario data is always for the previous day");
        }

        return builder.Build();
    }

    public Embed EmbedReportBuilder(ProvinceReport report, List<int> breakdown)
    {
        builder = new EmbedBuilder();
        builder.WithThumbnailUrl(DataLookups.flags[report.province]);
        DateTime date = DateTime.Now;

        if(date.Date == report.last_updated.Date){
            builder.WithTitle(DataLookups.provinces[report.province] + " Daily Report - " + report.last_updated.ToShortDateString() + " - " + report.last_updated.ToShortTimeString());
        }
        else{
            builder.WithTitle(DataLookups.provinces[report.province] + " Daily Report - " + date.ToShortDateString() + " - " + date.ToShortTimeString());

        }

        builder.AddField("New Cases", report.data[0].change_cases.ToString() ?? "0");
        builder.AddField("New Deaths", report.data[0].change_fatalities.ToString() ?? "0");
        builder.AddField("New Hospitalizations", report.data[0].change_hospitalizations.ToString() ?? "0");
        builder.AddField("New ICU Cases", report.data[0].change_criticals.ToString() ?? "0");
        builder.AddField("New Recoveries", report.data[0].change_recoveries.ToString() ?? "0");

        builder.AddField("Zone 1 (Moncton) Cases", breakdown[0] );
        builder.AddField("Zone 1 (Saint John) Cases", breakdown[1] );
        builder.AddField("Zone 1 (Fredericton) Cases", breakdown[2] );
        builder.AddField("Zone 1 (Edmunston) Cases", breakdown[3]);
        builder.AddField("Zone 1 (Campbellton) Cases", breakdown[4]);
        builder.AddField("Zone 1 (Bathurst) Cases", breakdown[5] );
        builder.AddField("Zone 1 (Miramichi) Cases", breakdown[6]);

        if(report.data[0].change_tests != 0){
            builder.AddField("Test Positivity Rate", CalculatePositivityRate(report.data[0].change_cases, report.data[0].change_tests) + "%");
        }
        else{
            builder.AddField("Test Positivity Rate", "Not Available");
        }
        
        builder.WithFooter(DateTime.Now.ToShortDateString());
        return builder.Build();
    }

    private string CalculatePositivityRate(int? cases, int tests){

        double x = (Convert.ToDouble(cases) / Convert.ToDouble(tests));
        x = x * 100;

        return Math.Round(x).ToString();
    }

    /// <summary>
    /// Creates a List with all the exposure locations.
    /// </summary>
    /// <param name="exposures">A List of List of strings with all of the exposures</param>
    /// <param name="footerDate">The date the list was last updated</param>
    /// <returns></returns>
    public List<Embed> EmbedExposureBuilder(List<List<string>> exposures, string footerDate)
    {
        List<Embed> embeds = new List<Embed>();
        builder = new EmbedBuilder();
        int count = exposures.Count;
        string thumbnail = DataLookups.flags["nb"];
        string title = "Zone 1 (Moncton) Covid Exposures";
        string footer = footerDate;
        builder.WithThumbnailUrl(thumbnail);
        builder.WithTitle(title);
        builder.WithFooter(footer);
        builder.WithColor(Color.DarkRed);
        //exposures.RemoveAt(count-1);

        foreach(List<string> s in exposures){
            string desc = "";
            foreach(string x in s){
                desc += x + Environment.NewLine;
            }
            //typeof(EmbedBuilder).GetField("_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(builder, desc); //This line of code bypasses the character limit set in Discord.Net as it hasn't been updated to accomodate the new limit.
            builder.WithDescription(desc);
            builder.WithThumbnailUrl(thumbnail);
            builder.WithTitle(title);
            builder.WithFooter(footer);
            builder.WithColor(Color.DarkRed);
            embeds.Add(builder.Build());
            builder = new EmbedBuilder();
        }
        return embeds;
    }

    /// <summary>
    /// Builds an Embed message with Provincial/Federal Vaccine Data
    /// </summary>
    /// <param name="report">A <c>ProvinceReport</c> object</param>
    /// <param name="date">A <c>DateTime</c> object</param>
    /// <returns><c>Embed</c></returns>
    public Embed EmbedVaccineBuilder(ProvinceReport report, DateTime date){
        builder = new EmbedBuilder();
        builder.WithThumbnailUrl(DataLookups.flags[report.province]);
        if(date.Date == report.last_updated.Date){
            builder.WithTitle(DataLookups.provinces[report.province] + " Covid Update - " + report.last_updated.ToShortDateString() + " - " + report.last_updated.ToShortTimeString());
        }
        else{
            builder.WithTitle(DataLookups.provinces[report.province] + " Covid Update - " + date.ToShortDateString() + " - " + date.ToShortTimeString());

        }
        builder.AddField("New Vaccinations (First Dose)", report.data[0].change_vaccinations);
        builder.AddField("New Fully Vaccinated", report.data[0].change_vaccinated);
        builder.AddField("Total # of people with 1 dose", (report.data[0].total_vaccinations - report.data[0].total_vaccinated));
        builder.AddField("Total Vaccinations", report.data[0].total_vaccinations);
        builder.AddField("Total Vaccinated", report.data[0].total_vaccinated);
        builder.AddField("Total Vaccines Distributed", report.data[0].total_vaccines_distributed);

        int? vaccinations = report.data[0].change_vaccinations;

        if(vaccinations >= 108284){
            builder.WithColor(Color.Green);
            builder.Build();
        }
        else if(vaccinations < 108284 && vaccinations >= 80000){
            builder.WithColor(Color.Gold);
            builder.Build();
        }
        else if(vaccinations < 80000 && vaccinations >= 30000){
            builder.WithColor(Color.Orange);
            builder.Build();
        }
        else{
            builder.WithColor(Color.Red);
            builder.Build();
        }

        if(report.province.Equals("on")){
            builder.WithFooter("* Ontario data is always for the previous day");
        }

        return builder.Build();
    }

    /// <summary>
    /// Builds an <c>Embed</c> message with the percentage of people vaccinated in each province
    /// </summary>
    /// <param name="reports">A <c>List</c> of <c>ProvinceReport</c></param>
    /// <returns></returns>
    public Embed EmbedAllPercentagesBuilder(List<ProvinceReport> reports)
    {
        builder = new EmbedBuilder();
        builder.WithThumbnailUrl(DataLookups.flags["All"]);
        builder.WithTitle("Percent Vaccinated by Province/Territory");

        foreach(ProvinceReport report in reports){
            if(report.province != "pei"){
                builder.AddField(DataLookups.provinces[report.province] + " Percentage", "1st Dose: " + Calculator.CalculateFirstDosePercentage(report) + "%" + Environment.NewLine + "2nd Dose: "+
                Calculator.CalculateFullyVaxxedPercentage(report) + "%");
            }
            
        }

        return builder.Build();
    }

    /// <summary>
    /// Builds an Embed message to present percentages for Provincial/Federal Data
    /// </summary>
    /// <param name="report">A <c>ProvinceReport</c> object</param>
    /// <param name="date">A <c>DateTime</c> object</param>
    /// <returns><c>Embed</c></returns>
    public Embed EmbedPercentBuilder(ProvinceReport report, DateTime date){
        builder = new EmbedBuilder();
        builder.WithThumbnailUrl(DataLookups.flags[report.province]);
        if(date.Date == report.last_updated.Date){
            builder.WithTitle(DataLookups.provinces[report.province] + " Covid Update - " + report.last_updated.ToShortDateString() + " - " + report.last_updated.ToShortTimeString());
        }
        else{
            builder.WithTitle(DataLookups.provinces[report.province] + " Covid Update - " + date.ToShortDateString() + " - " + date.ToShortTimeString());

        }
        if(report.province.Equals("All")){
            builder.AddField("Received at least one dose",Math.Round(Calculator.CalculatePercentOneDose(report.data[0]),2) + "%");
            builder.AddField("Fully Vaccinated",Math.Round(Calculator.CalculateFullyVaccinated(report.data[0]),2) + "%");
        }
        else{
            builder.AddField("Received at least one dose",Math.Round(Calculator.CalculatePercentOneDoseProvincial(report.data[0],report.province),2) + "%");
            builder.AddField("Fully Vaccinated",Math.Round(Calculator.CalculateFullyVaccinatedProvincial(report.data[0], report.province),2) + "%");
        }

        if(report.province.Equals("on")){
            builder.WithFooter("* Ontario data is always for the previous day");
        }
        

        return builder.Build();
    }

    /// <summary>
    /// Builds an Embed
    /// </summary>
    /// <param name="report"></param>
    /// <param name="date"></param>
    /// <returns></returns>
    public Embed EmbedPromiseBuilder(ProvinceReport report, DateTime date){
        builder = new EmbedBuilder();
        builder.WithThumbnailUrl(DataLookups.flags[report.province]);
        builder.WithTitle(DataLookups.provinces[report.province] + " Promise Update - " + report.last_updated.ToShortDateString() + " - " + report.last_updated.ToShortTimeString());

        builder.WithDescription("PM Trudeau has promised that 6 million doses of the vaccine will be administered by March 31st. To date, " + report.data[0].total_vaccinations + " doses have been administered" + Environment.NewLine + "The promise is " + Calculator.CalculatePromisePercent(report.data[0]) + "% complete");
        return builder.Build();
    }

    public Embed EmbedLinkBuilder(){
        builder = new EmbedBuilder();
        builder.WithTitle("Relevant Links");
        builder.WithThumbnailUrl(DataLookups.flags["All"]);
        builder.AddField("New Brunswick Recovery Plan", "https://www2.gnb.ca/content/gnb/en/corporate/promo/covid-19/recovery.html");
        builder.AddField("New Brunswick Covid Dashboard", "https://experience.arcgis.com/experience/8eeb9a2052d641c996dba5de8f25a8aa");
        builder.AddField("Government of New Brunswick Youtube Channel", "https://www.youtube.com/gnbca2");
        builder.AddField("Government of New Brunswick - Facebook", "https://www.facebook.com/GovNB");
        builder.AddField("Government of New Brunswick - Twitter", "https://twitter.com/Gov_NB");
        builder.AddField("New Brunswick Vaccine rollout plan", "https://www2.gnb.ca/content/gnb/en/corporate/promo/covid-19/nb-vaccine.html");
        builder.AddField("Canada Vaccine Schedule", "https://www.canada.ca/en/public-health/services/diseases/2019-novel-coronavirus-infection/prevention-risks/covid-19-vaccine-treatment/vaccine-rollout.html");
        return builder.Build();
    }

    public Embed EmbedHelpBuilder(){
        builder = new EmbedBuilder();
        builder.WithTitle("Covid Bot - Command List");
        builder.AddField("Get Cases by Province", "Example: !cases nb, !cases ontario");
        builder.AddField("Get Cases by Province on Specified Date", "Example: !cases nb 2021-01-01");
        builder.AddField("Get Number of Cases Nationwide", "Example: !cases");
        builder.AddField("Get Number of Vaccinations in a Province", "Example: !vaccine nb, !vaccine ontario");
        builder.AddField("Get Number of Vaccinations in all Provinces", "Example: !vaccine");
        builder.AddField("Get Percentage of Canadians Vaccinated", "Example: !percent");
        builder.AddField("Get Percentage of Canadians Vaccinated in a Province", "Example: !percent nb, !percent ontario");

        return builder.Build();
    }

    public Embed EmbedPromiseNBBuilder(ProvinceReport report, DateTime date){
        builder = new EmbedBuilder();
        string amount = String.Format(CultureInfo.InvariantCulture, "{0:#,0}", report.data[0].total_vaccinations - report.data[0].total_vaccinated);
        string population = String.Format(CultureInfo.InvariantCulture,"{0:#,0}", DataLookups.population["nb"]);
        builder.WithThumbnailUrl(DataLookups.flags[report.province]);
        builder.WithTitle(DataLookups.provinces[report.province] + " Promise Update - " + report.last_updated.ToShortDateString() + " - " + report.last_updated.ToShortTimeString());

        int newVaccinations = (int)(report.data[0].change_vaccinations - report.data[0].change_vaccinated);

        if(newVaccinations > Calculator.CalculateRate(report.data[0], Calculator.DaysUntilMidJune())){
            builder.WithDescription("New Brunswick has currently vaccinated " 
        + Calculator.CalculateNBPromisePercent(report.data[0]) + "% of its population(" + (report.data[0].total_vaccinations - report.data[0].total_vaccinated) 
        + ") with one dose of the vaccine. They have " + Calculator.DaysUntilMidJune().ToString() 
        + " days to give the rest of the population its first dose. New Brunswick has also fully vaccinated " 
        + Calculator.CalculateNBVaccinatedPercent(report.data[0]) + "% and has " 
        + Calculator.DaysUntilMidAugust().ToString() + " days to fully vaccinate everyone" +
        Environment.NewLine + "As of today, we should be vaccinating at a rate of " + Calculator.CalculateRate(report.data[0], Calculator.DaysUntilMidJune()) + " vaccinations per day" +
        " and  we have exceeded that with " + newVaccinations + " new vaccinations today!");
        builder.WithColor(Color.Green);
        }
        else{
            builder.WithDescription("New Brunswick has currently vaccinated " 
        + Calculator.CalculateNBPromisePercent(report.data[0]) + "% of its population(" + (report.data[0].total_vaccinations - report.data[0].total_vaccinated) 
        + ") with one dose of the vaccine. They have " + Calculator.DaysUntilMidJune().ToString() 
        + " days to give the rest of the population its first dose. New Brunswick has also fully vaccinated " 
        + Calculator.CalculateNBVaccinatedPercent(report.data[0]) + "% and has " 
        + Calculator.DaysUntilMidAugust().ToString() + " days to fully vaccinate everyone" +
        Environment.NewLine + "As of today, we should be vaccinating at a rate of " + Calculator.CalculateRate(report.data[0], Calculator.DaysUntilMidJune()) + " vaccinations per day." +
        " Unfortunately we have not hit the target, and have only had vaccinations for " + newVaccinations + " new people today");
        builder.WithColor(Color.DarkRed);
        }

        
        return builder.Build();
    }

    public Embed EmbedFreezerDosesBuilder(ProvinceReport report, DateTime date){
        builder = new EmbedBuilder();
        int number = Calculator.CalculateDosesInFreezer(report.data[0]);

        if(report.province == "All"){
            builder.WithTitle("All doses in freezer in Canada");
            builder.WithThumbnailUrl(DataLookups.flags["All"]);
            builder.WithDescription("As of today " + report.last_updated.ToShortDateString() + " there are " + String.Format(CultureInfo.InvariantCulture,"{0:#,0}", number) + " doses sitting in freezers across the country");
        }
        else{
            builder.WithTitle("All doses in " + DataLookups.provinces[report.province] +  " freezers ");
            builder.WithThumbnailUrl(DataLookups.flags[report.province]);
            builder.WithDescription("As of today " + report.last_updated.ToShortDateString() + " there are " + String.Format(CultureInfo.InvariantCulture,"{0:#,0}", number)+ " doses sitting in freezers in " + DataLookups.provinces[report.province]);
        }

        if(report.province.Equals("on")){
            builder.WithFooter("* Ontario data is always for the previous day");
        }
        return builder.Build();
        
    }

    public Embed EmbedVariantBuilder(){
        builder = new EmbedBuilder();
        builder.WithThumbnailUrl(DataLookups.flags["All"]);
        builder.WithTitle("Covid Variants");
        builder.AddField("Variant Alpha(B.1.1.7)", "Variant originating in The United Kingdom");
        builder.AddField("Variant Beta(B.1.351)", "Variant originating in South Africa");
        builder.AddField("Variant Gamma(P.1)", "Variant originating in Brazil");
        builder.AddField("Variant Delta(B.1.617)", "Variant originating in India");
        builder.AddField("Variant Eta(B.1.525)", "Variant originating in Nigeria");
        builder.AddField("Variant Episilon(B.1.427/B.1.429)", "Variant originating in South California, USA");
        builder.AddField("Variant Iota(B.1.526)", "Variant originating in New York, USA");
        builder.AddField("Variant Lambda(C.37)","Variant originating in Peru ");
        builder.AddField("Variant Zeta(P.2)", "Variant originating in Rio de Janeiro");
        builder.AddField("Variant Theta(P.3)", "Variant originating in The Phillipines");
        builder.AddField("Variant Kappa(B.1.617.1","Variant originating in India");
        

        return builder.Build();
    }

    public Embed EmbedNBZoneBuilder(){
        builder = new EmbedBuilder();
        builder.WithTitle("New Brunswick Health Zones");
        builder.WithThumbnailUrl(DataLookups.flags["nb"]);
        builder.AddField("Zone 1", "Moncton");
        builder.AddField("Zone 2", "Saint John");
        builder.AddField("Zone 3", "Fredericton");
        builder.AddField("Zone 4", "Edmunston");
        builder.AddField("Zone 5", "Campbellton");
        builder.AddField("Zone 6", "Bathurst");
        builder.AddField("Zone 7", "Miramichi");
        builder.WithImageUrl("https://media.socastsrm.com/wordpress/wp-content/blogs.dir/1460/files/2020/03/NB-Health-Zone-Map.png");
        return builder.Build();
    }

    public Embed EmbedDosesInLastWeek(List<ProvinceReport> reports){
        builder = new EmbedBuilder();
        builder.WithThumbnailUrl(DataLookups.flags[reports[0].province]);
        builder.WithTitle("Number of doses administered in " + DataLookups.provinces[reports[0].province] + " in the last 7 days");

        int num = 0;

        foreach(ProvinceReport report in reports){
            if(report.data.Count != 0 || report.data != null){
                num += (int)report.data[0].change_vaccinations;
            }
        }

        builder.WithDescription(String.Format(CultureInfo.InvariantCulture,"{0:#,0}", num) + " doses administered in the last week");

        return builder.Build();
    }

    public Embed EmbedProjectionBuilder(ProvinceReport report){

        builder = new EmbedBuilder();

        builder.WithThumbnailUrl(DataLookups.flags[report.province]);
        builder.WithTitle(DataLookups.provinces[report.province] + " Projection Update Update - " + report.last_updated.ToShortDateString() + " - " + report.last_updated.ToShortTimeString());

        double percentOfPop = DataLookups.population[report.province] * 0.75;
        int numDaysLeft = Calculator.DaysUntilAug2();
        int dosesDone = (int)(report.data[0].total_vaccinated);
        int dosesRemaining = (int)(percentOfPop - dosesDone);
        int dosesPerDay = dosesRemaining / numDaysLeft;
        
        builder.WithDescription("The Government of New Brunswick is projecting to hit 75% of the population vaccinated with the second dose by Aug 2nd. As of today, there are " + String.Format(CultureInfo.InvariantCulture,"{0:#,0}",dosesRemaining) + " doses remaining to be administered to hit the 75% goal. Going forward, we would need to administer " + String.Format(CultureInfo.InvariantCulture,"{0:#,0}",dosesPerDay) + " doses per day to meet our target.");
        return builder.Build();
    }

    public Embed EmbedProjectionBuilder(List<ProvinceReport> reports){
        builder = new EmbedBuilder();

        builder.WithThumbnailUrl(DataLookups.flags["nb"]);
        builder.WithTitle(DataLookups.provinces["nb"] + " Projection Update - " + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString());

        int totalSecondDoses = 0;
        int average = 0;
        double percPop = DataLookups.population["nb"] * 0.75;
        int numberRemaining = (int)percPop - (int)reports[0].data[0].total_vaccinated;
        int numDays = 0;
        foreach(ProvinceReport p in reports){
            totalSecondDoses += (int)p.data[0].change_vaccinated;
        }

        average = totalSecondDoses / 7;
        numDays = numberRemaining / average;

        DateTime projDate = DateTime.Now.AddDays(numDays);

        builder.WithDescription("The Government of New Brunswick is projecting to hit 75% of the population vaccinated with the second dose by Aug 2nd. As of today, there are " + String.Format(CultureInfo.InvariantCulture,"{0:#,0}",numberRemaining) + " doses remaining to be administered to hit the 75% goal. We are currently vaccinating at a rate of " + String.Format(CultureInfo.InvariantCulture, "{0:#,0}", average) + " doses per day, and are projecting to hit 75% fully vaccinated by " + projDate.ToLongDateString());
        return builder.Build();
    }
}