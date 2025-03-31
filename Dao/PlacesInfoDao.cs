using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using poc_recommended_trip.Models;

namespace poc_recommended_trip.Dao
{
    public class PlacesInfoDao
    {
        public PlacesInfoModel GetPlaceInfoByName(string destinationName)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM places_info WHERE name = @Name LIMIT 1";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", destinationName);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new PlacesInfoModel
                            {
                                PlaceId = Convert.ToInt32(reader["place_id"]),
                                Name = reader["name"].ToString(),
                                Latitude = Convert.ToDouble(reader["latitude"]),
                                Longitude = Convert.ToDouble(reader["longitude"]),
                                OsmType = reader["osm_type"].ToString(),
                                OsmId = Convert.ToInt32(reader["osm_id"]),
                                Importance = Convert.ToDouble(reader["importance"]),
                                DisplayName = reader["display_name"].ToString(),
                                BoundingBox = reader["bounding_box"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }

        public void InsertPlaceInfo(PlacesInfoModel placeInfo)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"
                    INSERT INTO places_info (place_id, name, latitude, longitude, osm_type, osm_id, importance, display_name, bounding_box) 
                    VALUES (@PlaceId, @Name, @Latitude, @Longitude, @OsmType, @OsmId, @Importance, @DisplayName, @BoundingBox)";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@PlaceId", placeInfo.PlaceId);
                    cmd.Parameters.AddWithValue("@Name", placeInfo.Name);
                    cmd.Parameters.AddWithValue("@Latitude", placeInfo.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", placeInfo.Longitude);
                    cmd.Parameters.AddWithValue("@OsmType", placeInfo.OsmType);
                    cmd.Parameters.AddWithValue("@OsmId", placeInfo.OsmId);
                    cmd.Parameters.AddWithValue("@Importance", placeInfo.Importance);
                    cmd.Parameters.AddWithValue("@DisplayName", placeInfo.DisplayName);
                    cmd.Parameters.AddWithValue("@BoundingBox", placeInfo.BoundingBox);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<PlacesInfoModel> ListAll()
        {
            List<PlacesInfoModel> places = new List<PlacesInfoModel>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM places_info";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            places.Add(new PlacesInfoModel
                            {
                                PlaceId = Convert.ToInt32(reader["place_id"]),
                                Name = reader["name"].ToString(),
                                Latitude = Convert.ToDouble(reader["latitude"]),
                                Longitude = Convert.ToDouble(reader["longitude"]),
                                OsmType = reader["osm_type"].ToString(),
                                OsmId = Convert.ToInt32(reader["osm_id"]),
                                Importance = Convert.ToDouble(reader["importance"]),
                                DisplayName = reader["display_name"].ToString(),
                                BoundingBox = reader["bounding_box"].ToString()
                            });
                        }
                    }
                }
            }

            return places;
        }

    }
}
