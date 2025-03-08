using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace SteamProxyLanCache
{
    internal class Tools
    {

        public static string getDepotFromURL(string rawUrl)
        {
            var depot = rawUrl.Remove(0, 7);
            depot = depot.Substring(0, depot.IndexOf("/"));

            return depot;

        }


        public static string processSteamUrl(string steamserver, string depoturl )
        {
            var url = steamserver;

            if (url.IndexOf("http://") == -1) {url = "http://" + steamserver; }

            var cachenr = (new Random().Next(1, 10));

            url = url.Replace("#", cachenr.ToString());
            url = url + depoturl;

            return url;
        }
    }




}
