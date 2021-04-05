using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public async Task<User> FindUser()
        {
            return await _dbSet.FirstOrDefaultAsync();
        }
    }
}
