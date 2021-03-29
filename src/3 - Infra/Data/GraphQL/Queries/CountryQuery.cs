using Data.GraphQL.Types;
using Domain.Interfaces.Services;
using GraphQL.Types;

namespace Data.GraphQL.Queries
{
    public class CountryQuery : ObjectGraphType
    {
        public CountryQuery(ICountryService countryService)
        {

            Field<ListGraphType<CountryType>>(
                "countries",
                resolve: context =>
                {
                    var contries = countryService.GetAll();
                    return contries;
                });

        }
    }
}
