using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SteamProxyLanCache
{
    public class Cache
    {

        public static string cs = "Data Source=cache.db;Cache=Shared;Pooling=True;Mode=ReadWriteCreate";
        //public static string cs = "Data Source=cache;Cache=Shared;Pooling=True;Mode=Memory";
        public SqliteConnection conn;
        public static List<string> filecache;

        public string sDateUsage = "";

        public static string sDateFormat = "yyyyMMdd";


        public Cache()
        {
            filecache = new List<string> { };

            using (conn = new SqliteConnection(cs))
            {


                conn.Open();

                var command = conn.CreateCommand();
                command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' AND name = 'cache'";

                using (var reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        string cs = "CREATE TABLE cache(name CHAR(254) PRIMARY KEY NOT NULL,accessed CHAR(24) NOT NULL)";
                        command = conn.CreateCommand();
                        command.CommandText = cs;
                        command.ExecuteNonQuery();
                    }


                }

                command = conn.CreateCommand();
                command.CommandText = "SELECT name,accessed FROM cache";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        filecache.Add(reader.GetString(0) + "-" + reader.GetString(1));
                    }

                }

            }

        }


        public void RegisterFile(string name)
        {
            sDateUsage = DateTime.Now.ToString(sDateFormat);


            if(!filecache.Contains(name + "-" + sDateUsage))
            {
                filecache.Add(name + "-" + sDateUsage);

                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open();

                }

                int iRows = -1;

                var command = conn.CreateCommand();
                command.CommandText = "INSERT OR REPLACE INTO cache(name,accessed)VALUES('" + name + "','" + sDateUsage + "')";
                iRows = command.ExecuteNonQuery();

            }


        }

        public int CleanCacheDBFiles(int days, string cachefilepath)
        {



            int filesDeleted = 0;

            if (days > 0) { days = days * -1; }
  

            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }


            //first clean the files which are in database

            string sql = "SELECT * FROM cache WHERE accessed <= '" + DateAndTime.Now.AddDays(days).ToString("yyyyMMdd") + "'";

            var command = conn.CreateCommand();
            command.CommandText = sql;
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string filename = cachefilepath + "\\" + reader.GetString(0) + ".dat";
                    DateTime fileaccessed = DateTime.ParseExact(reader.GetString(1), sDateFormat, new CultureInfo("nl-BE"));

                    if(fileaccessed <= DateAndTime.Now.AddDays(days)){
                        if (File.Exists(filename))
                        {
                            File.Delete(filename);
                        }

                        filesDeleted++;
                    }



                }

            }

            sql = "DELETE FROM cache WHERE accessed <= '" + DateAndTime.Now.AddDays(days).ToString("yyyyMMdd") + "'";
            command = conn.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();



            return filesDeleted;

        }

        public int CleanCacheDiskFiles(int days, string cachefilepath)
        {



            int filesDeleted = 0;

            if (days > 0) { days = days * -1; }


            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }


            //second loop through files on disk and see if they are in database, if not, delete the file;

            string[] files = Directory.GetFiles(cachefilepath, "*.dat", SearchOption.AllDirectories);

            foreach (string sFile in files)
            {
                string sFileName = sFile.Replace(cachefilepath, "");
                sFileName = sFileName.Replace(".dat", "");
                sFileName = sFileName.Replace("\\", "/");

                string sql = "SELECT * FROM cache WHERE name = '" + sFileName + "'";
                var command = conn.CreateCommand();
                command.CommandText = sql;
                var reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    File.Delete(sFile);
                    filesDeleted++;
                }

            }

            /*sql = "VACUUM cache.db";
            command = conn.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();*/


            return filesDeleted;

        }

    }
}
