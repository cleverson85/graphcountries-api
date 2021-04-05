using Domain.Models;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces.Services;

namespace Domain.Interfaces.Services
{
    public interface IUSerService : IBaseService<User>
    {
        Task<User> FindUser(User user);
    }
}
