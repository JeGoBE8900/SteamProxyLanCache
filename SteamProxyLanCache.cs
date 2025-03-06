using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Net;
using System.Drawing.Drawing2D;
using System.Text;
using Microsoft.VisualBasic.Logging;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net.NetworkInformation;
using System.Linq;
using System.Security.AccessControl;

namespace SteamProxyLanCache
{
    public partial class SteamProxyLanCache : Form
    {

        public const string httpEndpoint = "http://+:80/";
        public static string steamCacheURL;
        public static string steamLocalCacheFolder;
        public static bool steamLocalCacheEnabled;
        public static string[] depotFilter;

        public SteamProxyLanCache()
        {
            InitializeComponent();
        }

        private void SteamProxyLanCache_Load(object sender, EventArgs e)
        {
            LogLine("Program start", "");
            lblStatus.Text = "Stopped";

            getSteamServers();
            getConfig();

        }

        private void btnRun_Click(object sender, EventArgs e)
        {

            if (lblStatus.Text != "Running")
            {


                steamCacheURL = cboSteamContent.SelectedItem.ToString();
                steamLocalCacheFolder = txtLocalCache.Text;
                steamLocalCacheEnabled = chbLocalCacheUse.Checked;
                depotFilter  = txtDepotFilter.Text.Split(";");

                if (!Directory.Exists(steamLocalCacheFolder))
                {
                    MessageBox.Show("local cache folder doesn't exist");
                    return;
                }

                lblStatus.Text = "Running";
                btnRun.Text = "Stop";
                ThreadBody();


            }
            else
            {
                lblStatus.Text = "Stopped";
                btnRun.Text = "Start";
            }

        }

        private void LogLine(string logMessage1, string logMessage2)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            lvi.SubItems.Add(logMessage1);
            lvi.SubItems.Add(logMessage2);


            lvLog.Invoke(new Action(() => { lvLog.Items.Insert(0, lvi); }));


            if (lvLog.Items.Count > 1000)
            {
                lvLog.Invoke(new Action(() => { lvLog.Items[lvLog.Items.Count - 1].Remove(); }));
            }

        }


        private async void ThreadBody()
        {
            HttpListener listener = new HttpListener();

            if (!listener.IsListening)
            {

                listener.Prefixes.Add(httpEndpoint);
                listener.Start();

                while (lblStatus.Text == "Running")
                {
                    HttpListenerContext context = await listener.GetContextAsync();
                    var reqThProcessor = new Thread(() => ProcessRequest(context));
                    reqThProcessor.Start();
                }

                listener.Stop();
                listener = null;
            }
        }

        public void ProcessRequest(HttpListenerContext originalContext)
        {
            string rawUrl = originalContext.Request.RawUrl;


            var bFilterDepot = filterDepotCheck(originalContext.Request.RawUrl);



            var steamurl = steamCacheURL.ToLower();

            if (steamurl.IndexOf("http://") == -1) { steamurl = "http://" + steamurl; }

            var cachenr = (new Random().Next(1, 10));

            steamurl = steamurl.Replace("#", cachenr.ToString()); 

            steamurl = steamurl + rawUrl;

            string cachefilepath = steamLocalCacheFolder + rawUrl;


            if (File.Exists(cachefilepath + ".dat") && steamLocalCacheEnabled && bFilterDepot == false)
            {
                LogLine("Request " + originalContext.Request.RawUrl, "local hit");

                try
                {
                    var steamfile = new FileStream(cachefilepath + ".dat", FileMode.Open, FileAccess.Read);
                    steamfile.CopyTo(originalContext.Response.OutputStream);
                    originalContext.Response.OutputStream.Close();
                    originalContext.Response.Close();
                }
                catch (Exception e)
                {
                }


            }
            else
            {
                LogLine("Request " + originalContext.Request.RawUrl, steamurl);

                var relayRequest = (HttpWebRequest)WebRequest.Create(steamurl);
                relayRequest.KeepAlive = false;
                relayRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                relayRequest.UserAgent = originalContext.Request.UserAgent;

                var requestData = new RequestState(relayRequest, originalContext);
                relayRequest.BeginGetResponse(ResponseCallBack, requestData);

            }


        }

        private  void ResponseCallBack(IAsyncResult asynchronousResult)
        {
            var requestData = (RequestState)asynchronousResult.AsyncState;
            var originalResponse = requestData.context.Response;

            var bFilterDepot = filterDepotCheck(requestData.context.Request.RawUrl);

            string cachefilepath = steamLocalCacheFolder + requestData.context.Request.RawUrl;
            string cachedirpath = steamLocalCacheFolder + requestData.context.Request.RawUrl.Substring(0, requestData.context.Request.RawUrl.LastIndexOf('/') + 1);

                ///downloaden

                try
                {

                    using (var responseFromWebSiteBeingRelayed = (HttpWebResponse)requestData.webRequest.EndGetResponse(asynchronousResult))
                    {

                        using (var responseStreamFromWebSiteBeingRelayed = responseFromWebSiteBeingRelayed.GetResponseStream())
                        {

                            if (steamLocalCacheEnabled && bFilterDepot == false)                    {
                                //write to file

                                if (File.Exists(cachefilepath + ".tmp")) { File.Delete(cachefilepath + ".tmp"); }

                                if (!File.Exists(cachefilepath + ".dat"))
                                {

                                    Directory.CreateDirectory(cachedirpath);
                                    using (Stream cacheFile = File.OpenWrite(cachefilepath + ".tmp"))
                                    {
                                        responseStreamFromWebSiteBeingRelayed.CopyTo(cacheFile);
                                    }

                                    File.Move(cachefilepath + ".tmp", cachefilepath + ".dat");

                                }
                            }

                            responseStreamFromWebSiteBeingRelayed.CopyTo(originalResponse.OutputStream);
                            originalResponse.OutputStream.Close();
                            originalResponse.Close();
                      

                    }
                    }


                }
                catch (Exception ex)
                {

                    LogLine("Error", ex.Message);

                }




        }

        private Boolean filterDepotCheck(string rawUrl)
        {

            if (depotFilter.Contains("*"))
            {
                return false;
            }
            else
            {
                var depot = rawUrl.Remove(0, 7);
                depot = depot.Substring(0, depot.IndexOf("/"));

                if (depotFilter.Contains(depot))
                {
                    return false;
                }

            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1 = new FolderBrowserDialog();
            folderBrowserDialog1.InitialDirectory = txtLocalCache.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLocalCache.Text = folderBrowserDialog1.SelectedPath;
            }

        }


        private  void getSteamServers()
        {

            cboSteamContent.Items.Clear();


            //List<string> servers = new List<string>();


            if (!File.Exists(System.Environment.CurrentDirectory + "\\steamservers.json")) {
                cboSteamContent.Items.Add("cache10-ams1.steamcontent.com");
                LogLine("No steamservers.json","");
            }
            else
            {
                LogLine("Found steamservers.json", "");
                var sFile = File.ReadAllText(System.Environment.CurrentDirectory + "\\steamservers.json");

                JObject jResult = JObject.Parse(sFile) ;

                if (!jResult.ContainsKey("steamservers")) { return; }

                JArray jServers = jResult["steamservers"] as JArray;



                foreach (JObject jServer in jServers)
                {
                    cboSteamContent.Items.Add(jServer["url"]);
                }


            }

            cboSteamContent.SelectedIndex = 0;

        }

        private void getConfig()
        {
            if (!File.Exists(System.Environment.CurrentDirectory + "\\settings.json"))
            {
                LogLine("No settings.json found", "");
            }
            {
                LogLine("settings.json found", "");

                var sFile = File.ReadAllText(System.Environment.CurrentDirectory + "\\settings.json");
                JObject jResult = JObject.Parse(sFile);


                if (jResult.ContainsKey("steamcache")) {
                    cboSteamContent.Text = jResult["steamcache"].ToString();
                }

                if (jResult.ContainsKey("localcache"))
                {
                    txtLocalCache.Text = jResult["localcache"].ToString();
                }

                if (jResult.ContainsKey("filterdepot"))
                {
                    txtDepotFilter.Text = jResult["filterdepot"].ToString();
                }

                chbLocalCacheUse.Checked = false;

                if (jResult.ContainsKey("usecache"))
                {
                    if (jResult["usecache"].ToString() == "true")
                    {
                        chbLocalCacheUse.Checked = true;
                    }
                }


            }
        }
    }
}
