using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
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

            }

        }


        public void RegisterFile(string name)
        {
            sDateUsage = DateTime.Now.ToString("yyyy-MM-dd");

            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
                
            }

            if(!filecache.Contains(name + "-" + sDateUsage))
            {
                filecache.Add(name + "-" + sDateUsage);

                int iRows = -1;

                var command = conn.CreateCommand();
                command.CommandText = "INSERT OR REPLACE INTO cache(name,accessed)VALUES('" + name + "','" + sDateUsage + "')";
                iRows = command.ExecuteNonQuery();

            }


        }

        public Boolean CheckFile(string name, DateTime dateCheck, Boolean dDeleteFromCacheDB)
        {


            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }



            var command = conn.CreateCommand();
            command.CommandText = "SELECT name,accessed FROM cache WHERE name = '"+ name +"'";

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var filename = reader.GetString(0);
                    var fileaccessed = int.Parse(reader.GetString(1));




                }
            }

            return true;
        }
    }
}
