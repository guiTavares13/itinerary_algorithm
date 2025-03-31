using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_recommended_trip.Service
{
    public class GeoLocationService
    {
        private readonly HttpClient _httpClient;

        public GeoLocationService()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "poc_places_trip/1.0 (guiftt035@outlook.com)");
        }

        public async Task<string> GetRawApiResponseAsync(string placeName)
        {
            string url = $"https://nominatim.openstreetmap.org/search?q={Uri.EscapeDataString(placeName)}&format=json";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                    return "[]";
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter resposta da API: {ex.Message}");
                return "[]";
            }
        }
    }
}
