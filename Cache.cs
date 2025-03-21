using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

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

        public void UnregisterCacheFile(string name)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            var command = conn.CreateCommand();
            command.CommandText = "DELETE FROM cache WHERE name = '" + name + "'";
            command.ExecuteNonQuery();

            filecache.RemoveAll(c => c.Contains(name));

        }


        public Boolean CacheFileToUnregister(string name, DateTime dateCheck)
        {
            name = name.Replace(@"\", @"/");


            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            var command = conn.CreateCommand();
            command.CommandText = "SELECT name,accessed FROM cache WHERE name = '"+ name +"' LIMIT 1";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                   string filename = reader.GetString(0);
                   DateTime fileaccessed = DateTime.ParseExact(reader.GetString(1), sDateFormat, new CultureInfo("nl-BE"));


                    if (fileaccessed < dateCheck)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
  
                }
            }

            return true;
        }
    }
}
