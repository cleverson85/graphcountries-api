using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces.Services;

namespace Domain.Interfaces.Services
{
    public interface ICountryService : IBaseService<CountryData>
    {
        Task<IEnumerable<Country>> GetByCountryName(string countryName);
        Task<IEnumerable<Country>> GetByCapitalName(string capitalName);
        Task SaveCountry(Country entity);
        Task DeleteCountry(Country entity);
        Task UpdateCountry(Country entity);
    }
}
