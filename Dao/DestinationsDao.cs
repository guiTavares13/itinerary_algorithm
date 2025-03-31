using System;
using System.Collections.Generic;
using System.Data.SQLite;
using poc_recommended_trip.Models;

namespace poc_recommended_trip.Dao
{
    public class DestinationsDao
    {
        public void Insert(DestinationModel destinationModel)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"
            INSERT INTO destinations 
            (Destination, Region, Country, Category, Latitude, Longitude, Approximate_Annual_Tourists, 
             Currency, Majority_Religion, Famous_Foods, Language, Best_Time_to_Visit, Cost_of_Living, 
             Safety, Cultural_Significance, Description) 
            VALUES 
            (@Destination, @Region, @Country, @Category, @Latitude, @Longitude, @Approximate_Annual_Tourists, 
             @Currency, @Majority_Religion, @Famous_Foods, @Language, @Best_Time_to_Visit, @Cost_of_Living, 
             @Safety, @Cultural_Significance, @Description)";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Destination", destinationModel.Destination);
                    cmd.Parameters.AddWithValue("@Region", destinationModel.Region);
                    cmd.Parameters.AddWithValue("@Country", destinationModel.Country);
                    cmd.Parameters.AddWithValue("@Category", destinationModel.Category);
                    cmd.Parameters.AddWithValue("@Latitude", destinationModel.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", destinationModel.Longitude);
                    cmd.Parameters.AddWithValue("@Approximate_Annual_Tourists", ParseTouristNumber(destinationModel.ApproximateAnnualTourists)); // Sem .ToString()
                    cmd.Parameters.AddWithValue("@Currency", destinationModel.Currency);
                    cmd.Parameters.AddWithValue("@Majority_Religion", destinationModel.MajorityReligion);
                    cmd.Parameters.AddWithValue("@Famous_Foods", destinationModel.FamousFoods);
                    cmd.Parameters.AddWithValue("@Language", destinationModel.Language);
                    cmd.Parameters.AddWithValue("@Best_Time_to_Visit", destinationModel.BestTimeToVisit);
                    cmd.Parameters.AddWithValue("@Cost_of_Living", destinationModel.CostOfLiving);
                    cmd.Parameters.AddWithValue("@Safety", destinationModel.Safety);
                    cmd.Parameters.AddWithValue("@Cultural_Significance", destinationModel.CulturalSignificance);
                    cmd.Parameters.AddWithValue("@Description", destinationModel.Description);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<DestinationModel> ListAll()
        {
            var places = new List<DestinationModel>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM destinations";
                using (var cmd = new SQLiteCommand(sql, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        places.Add(new DestinationModel
                        {
                            Destination = reader["Destination"].ToString(),
                            Region = reader["Region"].ToString(),
                            Country = reader["Country"].ToString(),
                            Category = reader["Category"].ToString(),
                            Latitude = Convert.ToDouble(reader["Latitude"]),
                            Longitude = Convert.ToDouble(reader["Longitude"]),
                            ApproximateAnnualTourists = reader["Approximate_Annual_Tourists"].ToString(),
                            Currency = reader["Currency"].ToString(),
                            MajorityReligion = reader["Majority_Religion"].ToString(),
                            FamousFoods = reader["Famous_Foods"].ToString(),
                            Language = reader["Language"].ToString(),
                            BestTimeToVisit = reader["Best_Time_to_Visit"].ToString(),
                            CostOfLiving = reader["Cost_of_Living"].ToString(),
                            Safety = reader["Safety"].ToString(),
                            CulturalSignificance = reader["Cultural_Significance"].ToString(),
                            Description = reader["Description"].ToString()
                        });
                    }
                }
            }
            return places;
        }


        public void Update(DestinationModel destinationModel)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = @"
                    UPDATE destinations 
                    SET Destination = @Destination, Region = @Region, Country = @Country, Category = @Category, 
                        Latitude = @Latitude, Longitude = @Longitude, Approximate_Annual_Tourists = @Approximate_Annual_Tourists, 
                        Currency = @Currency, Majority_Religion = @Majority_Religion, Famous_Foods = @Famous_Foods, 
                        Language = @Language, Best_Time_to_Visit = @Best_Time_to_Visit, Cost_of_Living = @Cost_of_Living, 
                        Safety = @Safety, Cultural_Significance = @Cultural_Significance, Description = @Description 
                    WHERE id = @Id";

                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Destination", destinationModel.Destination);
                    cmd.Parameters.AddWithValue("@Region", destinationModel.Region);
                    cmd.Parameters.AddWithValue("@Country", destinationModel.Country);
                    cmd.Parameters.AddWithValue("@Category", destinationModel.Category);
                    cmd.Parameters.AddWithValue("@Latitude", destinationModel.Latitude);
                    cmd.Parameters.AddWithValue("@Longitude", destinationModel.Longitude);
                    cmd.Parameters.AddWithValue("@Approximate_Annual_Tourists", destinationModel.ApproximateAnnualTourists);
                    cmd.Parameters.AddWithValue("@Currency", destinationModel.Currency);
                    cmd.Parameters.AddWithValue("@Majority_Religion", destinationModel.MajorityReligion);
                    cmd.Parameters.AddWithValue("@Famous_Foods", destinationModel.FamousFoods);
                    cmd.Parameters.AddWithValue("@Language", destinationModel.Language);
                    cmd.Parameters.AddWithValue("@Best_Time_to_Visit", destinationModel.BestTimeToVisit);
                    cmd.Parameters.AddWithValue("@Cost_of_Living", destinationModel.CostOfLiving);
                    cmd.Parameters.AddWithValue("@Safety", destinationModel.Safety);
                    cmd.Parameters.AddWithValue("@Cultural_Significance", destinationModel.CulturalSignificance);
                    cmd.Parameters.AddWithValue("@Description", destinationModel.Description);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private int ParseTouristNumber(string tourists)
        {
            if (string.IsNullOrWhiteSpace(tourists))
                return 0;

            tourists = tourists.ToLower()
                               .Replace("(region-wide)", "")
                               .Replace(",", "")
                               .Trim();

            if (tourists.Contains("million"))
            {
                tourists = tourists.Replace("million", "").Trim();
                if (double.TryParse(tourists, out double result))
                {
                    return (int)(result * 1_000_000);
                }
            }
            else
            {
                if (int.TryParse(tourists, out int result))
                {
                    return result;
                }
            }

            return 0;
        }


        public void Delete(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "DELETE FROM destinations WHERE id = @Id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public DestinationModel FindById(int id)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM destinations WHERE id = @Id";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new DestinationModel
                            {
                                Destination = reader["Destination"].ToString(),
                                Description = reader["Description"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }
    }
}
