using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class CountryRepository : BaseRepository<CountryData>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<CountryData> GetCountryData()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }
    }
}
