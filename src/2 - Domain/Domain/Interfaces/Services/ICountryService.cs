using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces.Services;

namespace Domain.Interfaces.Services
{
    public interface ICountryService : IBaseService<ContryData>
    {
        public Task<IEnumerable<ContryData>> GetByCoutryName(string countryName);
        public Task<IEnumerable<ContryData>> GetByCapitalName(string capitalName);
    }
}
