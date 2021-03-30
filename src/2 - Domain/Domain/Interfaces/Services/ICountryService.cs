using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces.Services;

namespace Domain.Interfaces.Services
{
    public interface ICountryService : IBaseService<CountryData>
    {
        public Task<IEnumerable<Country>> GetByCountryName(string countryName);
        public Task<IEnumerable<Country>> GetByCapitalName(string capitalName);
        public Task SaveCountry(Country entity);
        public Task DeleteCountry(Country entity);
    }
}
