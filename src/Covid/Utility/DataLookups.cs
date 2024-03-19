using System.Collections.Generic;

public static class DataLookups{
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
        {"nu", "Nunavut"},
        {"All", "Canada"}
    }; 

    public static Dictionary<string,string> flags = new Dictionary<string, string>{
        {"nb", @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/fb/Flag_of_New_Brunswick.svg/100px-Flag_of_New_Brunswick.svg.png"},
        {"ns", @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c0/Flag_of_Nova_Scotia.svg/100px-Flag_of_Nova_Scotia.svg.png"},
        {"pei", @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/d7/Flag_of_Prince_Edward_Island.svg/100px-Flag_of_Prince_Edward_Island.svg.png"},
        {"nl", @"https://upload.wikimedia.org/wikipedia/commons/thumb/d/dd/Flag_of_Newfoundland_and_Labrador.svg/100px-Flag_of_Newfoundland_and_Labrador.svg.png"},
        {"qc", @"https://upload.wikimedia.org/wikipedia/commons/thumb/5/5f/Flag_of_Quebec.svg/100px-Flag_of_Quebec.svg.png"},
        {"on", @"https://upload.wikimedia.org/wikipedia/commons/thumb/8/88/Flag_of_Ontario.svg/100px-Flag_of_Ontario.svg.png"},
        {"mb", @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c4/Flag_of_Manitoba.svg/100px-Flag_of_Manitoba.svg.png"},
        {"sk", @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Flag_of_Saskatchewan.svg/100px-Flag_of_Saskatchewan.svg.png"},
        {"ab", @"https://upload.wikimedia.org/wikipedia/commons/thumb/f/f5/Flag_of_Alberta.svg/100px-Flag_of_Alberta.svg.png"},
        {"bc", @"https://upload.wikimedia.org/wikipedia/commons/thumb/b/b8/Flag_of_British_Columbia.svg/100px-Flag_of_British_Columbia.svg.png"},
        {"nt", @"https://upload.wikimedia.org/wikipedia/commons/thumb/c/c1/Flag_of_the_Northwest_Territories.svg/100px-Flag_of_the_Northwest_Territories.svg.png"},
        {"yt", @"https://upload.wikimedia.org/wikipedia/commons/thumb/6/69/Flag_of_Yukon.svg/100px-Flag_of_Yukon.svg.png"},
        {"nu", @"https://upload.wikimedia.org/wikipedia/commons/thumb/9/90/Flag_of_Nunavut.svg/100px-Flag_of_Nunavut.svg.png"},
        {"All", @"https://upload.wikimedia.org/wikipedia/en/thumb/c/cf/Flag_of_Canada.svg/100px-Flag_of_Canada.svg.png"}
    };

    public static Dictionary<string,int> population = new Dictionary<string, int>{
        {"nb", 750000},
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

      public static int NATIONAL_POPULATION = 38008005; 
}