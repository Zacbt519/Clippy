using System;
using System.Net;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Extensions;

public class NBGovService
{

    private string FIRE_BAN_NB = "https://www.gnb.ca/public/fire-feu/Maps/cat1.png";
    private string FIRE_BAN_NS = "https://novascotia.ca/natr/forestprotection/wildfire/burnsafe/images/burnsafeprovince.jpg";

    /// <summary>
    /// Downloads the NB Fire ban map
    /// </summary>
    public void GetNBFirebanImage()
    {
        using (WebClient client = new WebClient())
        {
            client.DownloadFile(new Uri(FIRE_BAN_NB), "src/Utility/Images/FirebanNB.png");
        }
    }

    /// <summary>
    /// Downloads the NS Fire ban map
    /// </summary>
    public void GetNSFirebanImage()
    {
        using (WebClient client = new WebClient())
        {
            client.DownloadFile(new Uri(FIRE_BAN_NS), "src/Utility/Images/FirebanNS.jpg");
        }
    }

}