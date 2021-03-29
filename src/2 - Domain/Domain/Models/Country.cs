using System.Collections.Generic;

namespace Domain.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public Flag Flag { get; set; }
        public string Capital { get; set; }
        public int Area { get; set; }
        public int Population { get; set; }
        public decimal PopulationDensity { get; set; }
        public IList<DistanceToOtherCountries> DistanceToOtherCountries { get; set; }
    }

    public class Flag
    {
        public string SvgFile { get; set; }
    }

    public class DistanceToOtherCountries
    {
        public string CountryName { get; set; }
        public decimal DistanceInKm { get; set; }
    }
}

