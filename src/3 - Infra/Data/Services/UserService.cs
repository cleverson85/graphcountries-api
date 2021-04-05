using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using System.Threading.Tasks;

namespace Data.Services
{
    public class UserService : BaseService<User>, IUSerService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository) : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> FindUser(User user)
        {
            return await _userRepository.FindUser();
        }
    }
}
