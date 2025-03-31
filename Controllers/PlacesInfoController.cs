using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using poc_recommended_trip.Dao;
using poc_recommended_trip.Models;
using poc_recommended_trip.Service;

namespace poc_recommended_trip.Controllers
{
    public class PlacesInfoController
    {
        private readonly DestinationsDao _destinationsDao;
        private readonly PlacesInfoDao _placesInfoDao;
        private readonly GeoLocationService _geoLocationService;

        public PlacesInfoController()
        {
            _destinationsDao = new DestinationsDao();
            _placesInfoDao = new PlacesInfoDao();
            _geoLocationService = new GeoLocationService();
        }

        public async Task<PlacesInfoModel> GetOrFetchPlaceInfo(string destinationName)
        {
            PlacesInfoModel placeInfo = _placesInfoDao.GetPlaceInfoByName(destinationName);

            if (placeInfo != null)
            {
                MessageBox.Show($"🔹 Informações já existem para {destinationName}, retornando do banco.");
                return placeInfo;
            }

            MessageBox.Show($"🔸 Nenhum dado encontrado para {destinationName}. Buscando na API...");

            string jsonResponse = await _geoLocationService.GetRawApiResponseAsync(destinationName);
            JArray data = JArray.Parse(jsonResponse);

            if (data.Count > 0)
            {
                var place = data[0];

                placeInfo = new PlacesInfoModel
                {
                    PlaceId = (int)place["place_id"],
                    Name = place["name"]?.ToString() ?? destinationName,
                    Latitude = double.Parse(place["lat"].ToString()),
                    Longitude = double.Parse(place["lon"].ToString()),
                    OsmType = place["osm_type"].ToString(),
                    OsmId = (int)place["osm_id"],
                    Importance = (double)place["importance"],
                    DisplayName = place["display_name"].ToString(),
                    BoundingBox = string.Join(",", place["boundingbox"])
                };

                _placesInfoDao.InsertPlaceInfo(placeInfo);

                MessageBox.Show($"✅ Dados salvos para {destinationName}!");
                return placeInfo;
            }

            MessageBox.Show($"❌ Nenhuma informação encontrada para {destinationName}.");
            return null;
        }

    }
}
