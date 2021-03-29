using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models;

namespace Data.Repositories
{
    public class CountryRepository : BaseRepository<ContryData>, ICountryRepository
    {
        public CountryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }
    }
}
