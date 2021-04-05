using System.Collections.Generic;

namespace Domain.Models
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public Flag Flag { get; set; }
        public string Capital { get; set; }
        public string Area { get; set; }
        public string Population { get; set; }
        public string PopulationDensity { get; set; }
        public IList<DistanceToOtherCountries> DistanceToOtherCountries { get; set; }
        public string OfficialLanguages { get; set; }
        public string TopLevelDomains { get; set; }
        public IList<Borders> Borders { get; set; }
        public bool Changed { get; set; }

        public Country()
        { }

        public Country(string name, string capital, string area, string population, string populationDensity, string officialLanguages, string topLevelDomains, int id) : base(id)
        {
            Name = name;
            Capital = capital;
            Area = area;
            Population = population;
            PopulationDensity = populationDensity;
            OfficialLanguages = officialLanguages;
            TopLevelDomains = topLevelDomains;
        }
    }

    public class Flag
    {
        public string svgFile { get; set; }
    }

    public class DistanceToOtherCountries
    {
        public string CountryName { get; set; }
        public string DistanceInKm { get; set; }
    }


    public class Borders
    {
        public string Name { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}

