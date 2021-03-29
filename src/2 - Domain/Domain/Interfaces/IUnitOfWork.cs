using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        DbContext GetContext();
        Task<IDbContextTransaction> InitTransacao();
        Task SendChanges();
    }
}
