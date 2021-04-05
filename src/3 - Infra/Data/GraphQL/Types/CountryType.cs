using Domain.Models;
using GraphQL.Types;

namespace Data.GraphQL.Types
{
    public class CountryType : ObjectGraphType<Country>
    {
        public CountryType()
        {
            Name = "Country";

            Field(c => c.Id);
            Field(c => c.Name);
            Field(c => c.Capital);
            Field(c => c.Flag);
            Field(c => c.Area);
            Field(c => c.Population);
            Field(c => c.PopulationDensity);
            Field(c => c.DistanceToOtherCountries);
        }
    }
}
