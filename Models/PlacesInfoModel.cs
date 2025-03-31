using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_recommended_trip.Models
{
    public class PlacesInfoModel
    {
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string OsmType { get; set; }
        public int OsmId { get; set; }
        public double Importance { get; set; }
        public string DisplayName { get; set; }
        public string BoundingBox { get; set; }
    }
}
