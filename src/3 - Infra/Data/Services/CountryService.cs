using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Services
{
    public class CountryService : BaseService<ContryData>, ICountryService
    {
        private readonly ICountryRepository _countryRepository;

        public CountryService(ICountryRepository countryRepository) : base(countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task<IEnumerable<ContryData>> GetByCapitalName(string capitalName)
        {
            return await _countryRepository.GetByExpression(c => c.JsonData.Contains(capitalName));
        }

        public async Task<IEnumerable<ContryData>> GetByCoutryName(string countryName)
        {
            return await _countryRepository.GetByExpression(c => c.JsonData.Contains(countryName));
        }
    }
}
