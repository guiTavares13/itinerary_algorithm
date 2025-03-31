using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using poc_recommended_trip.Models;
using Accord.MachineLearning;
using Accord.Math;
using poc_recommended_trip.Dao;

namespace poc_recommended_trip.MachineLearning
{
    public class KMeansClustering
    {

        /// <summary>
        /// 🔥 Algoritmo de clusterizacao K-menas
        /// </summary>
        public List<List<DestinationModel>> ClusterDestinations(string country, int days)
        {
            var destinations = GetDestinationsByCountry(country);
            if (destinations.Count == 0) return new List<List<DestinationModel>>();

            destinations = destinations.OrderByDescending(d => d.ApproximateAnnualTourists).ToList();

            int k = Math.Min(days, destinations.Count);
            var kmeans = new KMeans(k);

            double[][] data = destinations.Select(d => new double[] { d.Latitude, d.Longitude }).ToArray();
            var clusters = kmeans.Learn(data);
            int[] labels = clusters.Decide(data);

            var clusteredDestinations = new List<List<DestinationModel>>();
            for (int i = 0; i < k; i++)
            {
                clusteredDestinations.Add(new List<DestinationModel>());
            }

            for (int i = 0; i < destinations.Count; i++)
            {
                clusteredDestinations[labels[i]].Add(destinations[i]);
            }

            for (int i = 0; i < clusteredDestinations.Count; i++)
            {
                clusteredDestinations[i] = SolveTSP(clusteredDestinations[i]);
            }

            return clusteredDestinations;
        }


        private List<DestinationModel> GetDestinationsByCountry(string country)
        {
            var destinations = new List<DestinationModel>();
            using (var conn = Database.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM destinations WHERE Country = @Country ORDER BY Approximate_Annual_Tourists DESC";
                using (var cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Country", country);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            destinations.Add(new DestinationModel
                            {
                                Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                                Destination = reader["Destination"].ToString(),
                                Region = reader["Region"].ToString(),
                                Country = reader["Country"].ToString(),
                                Latitude = Convert.ToDouble(reader["Latitude"]),
                                Longitude = Convert.ToDouble(reader["Longitude"]),
                                ApproximateAnnualTourists = (reader["Approximate_Annual_Tourists"].ToString())
                            });
                        }
                    }
                }
            }
            return destinations;
        }

        /// <summary>
        /// 🔥 Algoritmo do Caixeiro Viajante usando o método do Vizinho Mais Próximo
        /// </summary>
        private List<DestinationModel> SolveTSP(List<DestinationModel> destinations)
        {
            if (destinations.Count <= 1) return destinations;

            List<DestinationModel> orderedRoute = new List<DestinationModel>();
            HashSet<int> visited = new HashSet<int>();

            // Começar pelo primeiro destino
            DestinationModel current = destinations[0];
            orderedRoute.Add(current);
            visited.Add(current.Id ?? 0);

            while (orderedRoute.Count < destinations.Count)
            {
                DestinationModel next = destinations
                    .Where(d => !visited.Contains(d.Id ?? 0)) 
                    .OrderBy(d => GetDistance(current, d))
                    .FirstOrDefault();

                if (next != null)
                {
                    orderedRoute.Add(next);
                    visited.Add(next.Id ?? 0);
                    current = next;
                }
            }

            return orderedRoute;
        }

        /// <summary>
        /// Calcula a distância entre dois destinos usando a fórmula de Haversine
        /// </summary>
        private double GetDistance(DestinationModel d1, DestinationModel d2)
        {
            double R = 6371; 
            double lat1 = DegreeToRadian(d1.Latitude);
            double lon1 = DegreeToRadian(d1.Longitude);
            double lat2 = DegreeToRadian(d2.Latitude);
            double lon2 = DegreeToRadian(d2.Longitude);

            double dlat = lat2 - lat1;
            double dlon = lon2 - lon1;

            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c; // Distância em km
        }

        private double DegreeToRadian(double degree)
        {
            return degree * Math.PI / 180.0;
        }
    }
}
