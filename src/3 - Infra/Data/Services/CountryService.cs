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
            return await Task.FromResult(jsonList.Where(c => c.Capital.Contains(capitalName, StringComparison.InvariantCultureIgnoreCase)));
        }

        public async Task<IEnumerable<Country>> GetByCountryName(string countryName)
        {
            IList<Country> jsonList = await GetJsonCountryList();
            return await Task.FromResult(jsonList.Where(c => c.Name.Contains(countryName, StringComparison.InvariantCultureIgnoreCase)));
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

            jsonList.Add(await ConfigureEntityId(entity));
            country.JsonData = JsonSerializer.Serialize(jsonList);

            await Save(country);
        }

        public async Task DeleteCountry(int id)
        {
            IList<Country> jsonList = await GetJsonCountryList();
            var entity = await GetCountryById(id);

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
            var entityToRemove = jsonList.Where(c => c.Id == entity.Id).FirstOrDefault();

            if (jsonList.Remove(entityToRemove))
            {
                jsonList.Add(entity);
                var country = await _countryRepository.GetCountryData();
                country.JsonData = JsonSerializer.Serialize(jsonList);

                await Save(country);
            }
        }

        public async Task<IList<Country>> GetCountries()
        {
            return await GetJsonCountryList();
        }

        public async Task<IList<Country>> GetWithPages(int pageIndex, int pageSize)
        {
            var list = await GetJsonCountryList();
            return await Task.FromResult(list.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList());
        }

        private async Task<IList<Country>> GetJsonCountryList()
        {
            var country = await _countryRepository.GetCountryData();

            if (country == null)
            {
                return new List<Country>();
            }

            return await Task.FromResult(JsonSerializer.Deserialize<IList<Country>>(country?.JsonData));
        }

        private async Task<Country> ConfigureEntityId(Country entity)
        {
            if (entity.Id == 0)
            {
                entity.Id = new Random().Next(500, 1000);
            }

            return await Task.FromResult(entity);
        }

        public async Task<int> GetCount()
        {
            var list = await GetJsonCountryList();
            return await Task.FromResult(list.Count);
        }

        public async Task<Country> GetCountryById(int id)
        {
            var country = await _countryRepository.GetCountryData();
            var jsonList = JsonSerializer.Deserialize<IList<Country>>(country.JsonData);
            return await Task.FromResult(jsonList.Where(c => c.Id == id).FirstOrDefault());
        }
    }
}
