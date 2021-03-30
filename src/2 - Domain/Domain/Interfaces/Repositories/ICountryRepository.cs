using Domain.Models;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces.Repository;

namespace Domain.Interfaces.Repositories
{
    public interface ICountryRepository : IBaseRepository<CountryData>
    {
        Task<CountryData> GetCountryData();
    }
}
