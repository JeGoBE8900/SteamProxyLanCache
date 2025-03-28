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
using System.Drawing;
using Microsoft.VisualBasic;
using System.Security.Policy;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Threading;

namespace SteamProxyLanCache
{
    public partial class SteamProxyLanCache : Form
    {

        public const string httpEndpoint = "http://+:80/";
        public static string steamCacheURL;
        public static string steamLocalCacheFolder;
        public static bool steamLocalCacheEnabled;
        public static string[] depotFilter;
        public static HttpListener listener;
        public static int daysRemoval;
        public static bool forceBypassCache;


        public static CancellationTokenSource ctsDeleteUnusedDepots;
        public static CancellationToken ctTokenDeleteUnusedDepots;

        public static CancellationTokenSource ctsCheckDiskSpace;
        public static CancellationToken ctTokenCheckDiskSpace;

        public static Cache oCacheDB;


        public SteamProxyLanCache()
        {
            InitializeComponent();
        }


        private void SteamProxyLanCache_Load(object sender, EventArgs e)
        {
            LogLine("Program start", "");
            lblStatus.Text = "Stopped";

            oCacheDB = new Cache();
            LogLine("initialize database", "");


            forceBypassCache = false;

            CheckDNS();

            getSteamServers();
            getConfig();
            startTrayIcon();

            if (chbMinize.Checked)
            {
                this.WindowState = FormWindowState.Minimized;
            }


            if (chbAutoStart.Checked)
            {
                LogLine("auto start", "");
                StartStopProxyServer();
            }


            /*ctsDeleteUnusedDepots = new CancellationTokenSource();
            ctTokenDeleteUnusedDepots = ctsDeleteUnusedDepots.Token;

            Task taskDeleteUnusedDepots = new Task(() => { deleteUnusedDepots(); }, ctTokenDeleteUnusedDepots);
            taskDeleteUnusedDepots.Start();*/

            ctsCheckDiskSpace = new CancellationTokenSource();
            ctTokenCheckDiskSpace = ctsCheckDiskSpace.Token;

            Task taskCheckDiskSpace = new Task(() => { checkDiskSpace(); }, ctTokenCheckDiskSpace);
            taskCheckDiskSpace.Start();


            txtInfo.Text = ControlChars.NewLine + "See https://github.com/JeGoBE8900/SteamProxyLanCache";
        }



        private void StartStopProxyServer()
        {
            if (lblStatus.Text != "Running" && CheckDNS())
            {


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


                listener.Stop();
                LogLine("Listener stopped", "");
            }

            tsmiStatus.Text = lblStatus.Text;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            getConfig();
            StartStopProxyServer();


        }

        private Boolean CheckDNS()
        {

            try
            {
                var ip = Dns.GetHostEntry("lancache.steamcontent.com");
                var s = "DNS lancache.steamcontent.com OK: " + ip.AddressList[0];
                lblDnsStatus.Text = s;
                LogLine(s, "");
                return true;
            }
            catch (Exception)
            {
                var s = "DNS Issue !!! Can't resolve lancache.steamcontent.com";
                lblDnsStatus.Text = s;
                LogLine(s, "");
                btnRun.Enabled = false;

            }

            return false;


        }


        private void LogLine(string logMessage1, string logMessage2)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            lvi.SubItems.Add(logMessage1);
            lvi.SubItems.Add(logMessage2);

            try
            {
                lvLog.Invoke(new Action(() => { lvLog.Items.Insert(0, lvi); }));


                if (lvLog.Items.Count > 1000)
                {
                    lvLog.Invoke(new Action(() => { lvLog.Items[lvLog.Items.Count - 1].Remove(); }));
                }
            }
            catch (Exception) { }


        }


        private async void ThreadBody()
        {
            if (listener == null)
            {
                listener = new HttpListener();
                
            }


            if (!listener.IsListening)
            {
                try
                {
                    listener.Prefixes.Add(httpEndpoint);
                    listener.Start();
                    LogLine("httpListener start", "");
                }
                catch (Exception ex)
                {
                    LogLine("httpListener failed to start", ex.ToString());
                    lblStatus.Text = "Stopped";
                    btnRun.Text = "Start";
                }


                while (lblStatus.Text == "Running")
                {

                    try
                    {
                        HttpListenerContext context = await listener.GetContextAsync();
       
                     
                        var reqThProcessor = new Thread(() => ProcessRequest(context));
                        reqThProcessor.Start();
                    }
                    catch (Exception ex) { }

                }

                try
                {
                    listener.Stop();
                    LogLine("httpListener stop", "");
                    
                }catch(Exception ex)
                {

                }

                lblStatus.Text = "Stopped";
                btnRun.Text = "Start";


            }


        }

        public void ProcessRequest(HttpListenerContext originalContext)
        {
            string rawUrl = originalContext.Request.RawUrl;
            var bFilterDepot = filterDepotCheck(originalContext.Request.RawUrl);
            var steamurl = Tools.processSteamUrl(steamCacheURL, rawUrl);
            string cachefilepath = steamLocalCacheFolder + rawUrl;
            var depot = Tools.getDepotFromURL(rawUrl);


            oCacheDB.RegisterFile(rawUrl);


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
                    LogLine("Request " + originalContext.Request.RawUrl, e.ToString());
                }


            }
            else
            {
                LogLine("Request " + originalContext.Request.RawUrl, steamurl);

                var relayRequest = (HttpWebRequest)WebRequest.Create(steamurl);
                relayRequest.KeepAlive = true;
                relayRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
                relayRequest.UserAgent = originalContext.Request.UserAgent;
                
                for(int i = 0; i<originalContext.Request.Headers.Count;i++)
                {
                    relayRequest.Headers.Add(originalContext.Request.Headers.Keys[i], originalContext.Request.Headers[i]);
                }

                var requestData = new RequestState(relayRequest, originalContext);
                relayRequest.BeginGetResponse(ResponseCallBack, requestData);

            }


        }

        private void ResponseCallBack(IAsyncResult asynchronousResult)
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

                        if (steamLocalCacheEnabled && bFilterDepot == false && forceBypassCache == false)
                        {
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

                LogLine(requestData.webRequest.Address.OriginalString, ex.Message);
                
                originalResponse.OutputStream.Close();
                originalResponse.Close();

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
                var depot = Tools.getDepotFromURL(rawUrl);

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


        private void getSteamServers()
        {

            cboSteamContent.Items.Clear();


            //List<string> servers = new List<string>();


            if (!File.Exists(System.Environment.CurrentDirectory + "\\steamservers.json"))
            {
                cboSteamContent.Items.Add("cache10-ams1.steamcontent.com");
                LogLine("No steamservers.json", "");
            }
            else
            {
                LogLine("Found steamservers.json", "");
                var sFile = File.ReadAllText(System.Environment.CurrentDirectory + "\\steamservers.json");

                JObject jResult = JObject.Parse(sFile);

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


                if (jResult.ContainsKey("steamcache"))
                {
                    cboSteamContent.Text = jResult["steamcache"].ToString();
                    steamCacheURL = cboSteamContent.Text;
                }

                if (jResult.ContainsKey("localcache"))
                {
                    txtLocalCache.Text = jResult["localcache"].ToString();
                    steamLocalCacheFolder = txtLocalCache.Text;
                }

                if (jResult.ContainsKey("filterdepot"))
                {
                    txtDepotFilter.Text = jResult["filterdepot"].ToString();
                    depotFilter = txtDepotFilter.Text.Split(";");
                }

                chbLocalCacheUse.Checked = false;

                if (jResult.ContainsKey("usecache"))
                {
                    if (jResult["usecache"].ToString().ToLower() == "true")
                    {
                        chbLocalCacheUse.Checked = true;
                    }
                }
                steamLocalCacheEnabled = chbLocalCacheUse.Checked;

                if (jResult.ContainsKey("autostart"))
                {
                    if (jResult["autostart"].ToString().ToLower() == "true")
                    {
                        chbAutoStart.Checked = true;
                    }
                    else { chbAutoStart.Checked = false; }
                }
                else { chbAutoStart.Checked = false; }


                if (jResult.ContainsKey("minimizeatstart"))
                {
                    if (jResult["minimizeatstart"].ToString().ToLower() == "true")
                    {
                        chbMinize.Checked = true;
                    }
                    else { chbMinize.Checked = false; }
                }
                else { chbMinize.Checked = false; }


                if (jResult.ContainsKey("unuseddaysremoval"))
                {
                    try
                    {
                        nudKeepUnusedDays.Value = (decimal)jResult["unuseddaysremoval"];

                    }
                    catch
                    {
                        LogLine("issue with unuseddatsremoval in settings.json", "");
                        nudKeepUnusedDays.Value = 7;
                    }

                }
                else { nudKeepUnusedDays.Value = 7; }
                daysRemoval = Convert.ToInt32(nudKeepUnusedDays.Value);

            }
        }

        private void startTrayIcon()
        {
            nfTray.Visible = true;


        }

        private void SteamProxyLanCache_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                // Do your action
                this.ShowInTaskbar = false;
            }
            else
            {
                this.ShowInTaskbar = true;
            }
        }

        private void nfTray_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    this.WindowState = FormWindowState.Minimized;
                }
            }



        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            JObject settings = new JObject(
                new JProperty("steamcache", cboSteamContent.Text),
                new JProperty("localcache", txtLocalCache.Text),
                new JProperty("usecache", chbLocalCacheUse.Checked.ToString()),
                new JProperty("filterdepot", txtDepotFilter.Text),
                new JProperty("autostart", chbAutoStart.Checked.ToString()),
                new JProperty("unuseddaysremoval", nudKeepUnusedDays.Value.ToString()),
                new JProperty("minimizeatstart", chbMinize.Checked.ToString())
                );

            File.WriteAllText(System.Environment.CurrentDirectory + "\\settings.json", settings.ToString());

            LogLine("Settings saved", "");

            if(lblStatus.Text == "Running")
            {
                if (MessageBox.Show("Stop HTTP Listener?", "", MessageBoxButtons.YesNo) == DialogResult.Yes )
                {
                    listener.Stop();


                    btnRun.Text = "Start";
                    lblStatus.Text = "Stopped";
                }
            }


        }

        #region diskspace
        private void checkDiskSpace()
        {
            try
            {
                while (!ctTokenCheckDiskSpace.IsCancellationRequested)
                {
                    var allDrives = DriveInfo.GetDrives()
                                                .Where(x => x.IsReady)
                                                .OrderBy(x => x.Name)
                                                .ToList();

                    lvDiskSpace.Invoke(new Action(() => { lvDiskSpace.Items.Clear(); }));

                    foreach (DriveInfo d in allDrives)
                    {
                        
                        double usedspace =  (d.TotalSize - d.TotalFreeSpace) / 1024 / 1024 / 1024;
                        double totalspace = d.TotalSize / 1024 / 1024 / 1024;

                        double prct = (100 / (float)d.TotalSize * (d.TotalSize - d.TotalFreeSpace));


                        ListViewItem item = new ListViewItem(d.Name);
                        item.Tag = d;
                        item.SubItems.Add(d.VolumeLabel);
                        item.SubItems.Add(totalspace.ToString() + " Gb");
                        item.SubItems.Add(usedspace.ToString() + " Gb");
                        item.SubItems.Add(Math.Round(prct).ToString() + "%");

                        if (steamLocalCacheFolder != null)
                        {
                            if (d.Name.Substring(0, 1).ToLower() == steamLocalCacheFolder.Substring(0, 1).ToLower())
                            {
                                item.SubItems.Add(steamLocalCacheFolder);

                                if (prct >= 95 && lblStatus.Text == "Running")
                                {
                                    forceBypassCache = true;
                                    LogLine("Not enough disk space, bypassing cache", "Should be below 95%");
                                }
                                else
                                {
                                    forceBypassCache = false;
                                }
                            }
                        }

                        lvDiskSpace.Invoke(new Action(() => { lvDiskSpace.Items.Add(item); }));
                    }

                    var cancellationTriggered = ctTokenCheckDiskSpace.WaitHandle.WaitOne(5000);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void deleteUnusedDepots()
        {

            while (!ctTokenDeleteUnusedDepots.IsCancellationRequested)
            {

                //ctTokenDeleteUnusedDepots.WaitHandle.WaitOne(1000 * 30);

              

                ctTokenDeleteUnusedDepots.WaitHandle.WaitOne(1000 * 60 * 60);
            }

        }

        private void CleanFilesFromDisk()
        {
            
            if (!Directory.Exists(steamLocalCacheFolder)) {
                LogLine("Ignore cleaning cache because there is no cache", "");
                return;
            }
            if (daysRemoval == 0){
                LogLine("Ignore cleaning cache because days to keep is 0", "");
                return; 
            }

            LogLine("Start cleaning cache", "");

            int negDaysRemoval = 0;
            if (daysRemoval > 0) { negDaysRemoval = daysRemoval * -1; }
            else { negDaysRemoval = daysRemoval; }

            string[] files = Directory.GetFiles(steamLocalCacheFolder, "*.dat", SearchOption.AllDirectories);
            LogLine(files.Length + " files in cache", "");

            int filesDeleted = 0;

            foreach (string sFile in files)
            {
                string sFileName = sFile.Replace(steamLocalCacheFolder, "");
                sFileName = sFileName.Replace(".dat", "");

                Boolean bCacheFileToUnregister = oCacheDB.CacheFileToUnregister(sFileName, DateAndTime.Now.AddDays(negDaysRemoval));

                if (bCacheFileToUnregister)
                {
                    File.Delete(sFile);
                    oCacheDB.UnregisterCacheFile(sFileName);
                    filesDeleted ++;

                }

            }

            LogLine(filesDeleted + " deleted from cache", "");
            LogLine("End cleaning cache", "");

        }

        private void btnCleanFiles_Click(object sender, EventArgs e)
        {
            CleanFilesFromDisk();
        }

        #endregion diskspace

        private void SteamProxyLanCache_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void nfTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void steamProxyLanCachToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }


    }
}
