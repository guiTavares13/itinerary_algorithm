using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace poc_recommended_trip.Models
{
    public class DestinationModel
    {
        public int? Id { get; set; }

        [Name("Destination")]
        public string Destination { get; set; }

        [Name("Region")]
        public string Region { get; set; }

        [Name("Country")]
        public string Country { get; set; }

        [Name("Category")]
        public string Category { get; set; }

        [Name("Latitude")]
        public double Latitude { get; set; }

        [Name("Longitude")]
        public double Longitude { get; set; }

        [Name("Approximate Annual Tourists")]
        public string ApproximateAnnualTourists { get; set; }

        [Name("Currency")]
        public string Currency { get; set; }

        [Name("Majority Religion")]
        public string MajorityReligion { get; set; }

        [Name("Famous Foods")]
        public string FamousFoods { get; set; }

        [Name("Language")]
        public string Language { get; set; }

        [Name("Best Time to Visit")]
        public string BestTimeToVisit { get; set; }

        [Name("Cost of Living")]
        public string CostOfLiving { get; set; }

        [Name("Safety")]
        public string Safety { get; set; }

        [Name("Cultural Significance")]
        public string CulturalSignificance { get; set; }

        [Name("Description")]
        public string Description { get; set; }
    }
}
