using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Services
{
    public class CountryService : BaseService<CountryData>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository) : base(countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<Country>> GetByCapitalName(string capitalName)
        {
            IList<Country> jsonList = await GetJsonCountryList();
            return jsonList.Where(c => c.Capital.Contains(capitalName, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task<IEnumerable<Country>> GetByCountryName(string countryName)
        {
            IList<Country> jsonList = await GetJsonCountryList();
            return jsonList.Where(c => c.Name.Contains(countryName, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task SaveCountry(Country entity)
        {
            IList<Country> jsonList = new List<Country>();

            var country = await _countryRepository.GetCountryData();

            if (country != null)
            {
                jsonList = JsonSerializer.Deserialize<IList<Country>>(country.JsonData);
            }
            else
            {
                country = new CountryData();
            }

            jsonList.Add(entity);
            country.JsonData = JsonSerializer.Serialize(jsonList);

            await Save(country);
        }

        public async Task DeleteCountry(Country entity)
        {
            IList<Country> jsonList = await GetJsonCountryList();

            if (jsonList.Remove(entity))
            {
                var country = await _countryRepository.GetCountryData();
                country.JsonData = JsonSerializer.Serialize(jsonList);

                await Save(country);
            }
        }

        public async Task UpdateCountry(Country entity)
        {
            IList<Country> jsonList = await GetJsonCountryList();

            if (jsonList.Remove(entity))
            {
                var country = await _countryRepository.GetCountryData();
                country.JsonData = JsonSerializer.Serialize(jsonList);

                await Save(country);
            }
        }

        private async Task<IList<Country>> GetJsonCountryList()
        {
            var country = await _countryRepository.GetCountryData();
            return JsonSerializer.Deserialize<IList<Country>>(country.JsonData);
        }
    }
}
