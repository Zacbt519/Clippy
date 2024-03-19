using System;
using System.Collections.Generic;

public static class ParamterValidation{

    public static Dictionary<string,string> provinces = new Dictionary<string, string>{
        {"nb", "New Brunswick"},
        {"ns", "Nova Scotia"},
        {"pei", "Prince Edward Island"},
        {"nl", "Newfoundland & Labrador"},
        {"qc", "Québec"},
        {"on", "Ontario"},
        {"mb", "Manitoba"},
        {"sk", "Saskatchewan"},
        {"ab", "Alberta"},
        {"bc", "British Columbia"},
        {"nt", "Northwest Territories"},
        {"yt", "Yukon"},
        {"nu", "Nunavut"}
    }; 

    public static Dictionary<string,int> population = new Dictionary<string, int>{
        {"nb", 781315},
        {"ns", 979115},
        {"pei", 159713},
        {"nl", 520998},
        {"qc", 8575779},
        {"on", 14733119},
        {"mb", 1379584},
        {"sk", 1177884},
        {"ab", 4428112},
        {"bc", 5145851},
        {"nt", 45074},
        {"yt", 42176},
        {"nu", 32985}
    }; 

    public static bool IsValidProvinceCode(string province){
        string code = "";
        foreach(KeyValuePair<string, string> pair in provinces){
            if(pair.Key.Equals(province, StringComparison.InvariantCultureIgnoreCase) || pair.Value.Equals(province, StringComparison.InvariantCultureIgnoreCase) ){
                code = pair.Key;
            }
        }

        if(string.IsNullOrWhiteSpace(code)){
            return false;
        }
        else{
            return true;
        }
    }

    public static DateTime IsValidDate(string date){
        if(string.IsNullOrWhiteSpace(date)){
            return DateTime.Now;
        }
        else{
            if(DateTime.TryParse(date, out DateTime parsedDate)){
                return parsedDate;
            }
            else{
                return DateTime.Now;
            }
        }
        
    }

    public static string ValidateParameterInput(string input){
        string value = "";
        foreach(KeyValuePair<string, string> pair in DataLookups.provinces){
            if(pair.Key.Equals(input, StringComparison.InvariantCultureIgnoreCase)){
                value = pair.Key;
            }
            else if(pair.Value.Equals(input, StringComparison.InvariantCultureIgnoreCase)){
                value = pair.Key;
            }
        }

        return value;
    }
}