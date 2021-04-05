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
        Task DeleteCountry(int id);
        Task UpdateCountry(Country entity);
        Task<IList<Country>> GetCountries();
        Task<IList<Country>> GetWithPages(int pageIndex, int pageSize);
        Task<int> GetCount();
        Task<Country> GetCountryById(int id);
    }
}
