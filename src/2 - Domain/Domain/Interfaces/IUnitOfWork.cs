using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext GetContext();
        Task<IDbContextTransaction> InitTransacao();
        Task SendChanges();
        Task Commit();
    }
}
