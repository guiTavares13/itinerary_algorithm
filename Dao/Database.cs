using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_recommended_trip.Dao
{
    public class Database
    {
        private static readonly string _dbPath = "tourism.db";
        private static readonly string _connectionString = $"Data Source={_dbPath};Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(_connectionString);
        }

        public static void Initialize()
        {
            if (!File.Exists(_dbPath))
            {
                SQLiteConnection.CreateFile(_dbPath);
                Console.WriteLine("Banco de dados SQLite criado.");
            }

            using (var conn = GetConnection())
            {
                conn.Open();
                string sql = @"
                    CREATE TABLE IF NOT EXISTS destinations (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Destination TEXT,
                        Region TEXT,
                        Country TEXT,
                        Category TEXT,
                        Latitude REAL,
                        Longitude REAL,
                        Approximate_Annual_Tourists TEXT,
                        Currency TEXT,
                        Majority_Religion TEXT,
                        Famous_Foods TEXT,
                        Language TEXT,
                        Best_Time_to_Visit TEXT,
                        Cost_of_Living TEXT,
                        Safety TEXT,
                        Cultural_Significance TEXT,
                        Description TEXT
                    );

                    CREATE TABLE IF NOT EXISTS places_info (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        place_id INTEGER,
                        name TEXT,
                        latitude REAL,
                        longitude REAL,
                        osm_type TEXT,
                        osm_id INTEGER,
                        importance REAL,
                        display_name TEXT,
                        bounding_box TEXT
                    );";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
