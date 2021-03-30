
using Data.Contexts;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly Context _context;
        private bool disposedValue;

        public IDbContextTransaction Transaction { get; private set; }

        public UnitOfWork(Context context)
        {
            _context = context;
        }

        public DbContext GetContext()
        {
            return _context;
        }

        public async Task<IDbContextTransaction> InitTransacao()
        {
            if (Transaction == null)
            {
                Transaction = await _context.Database.BeginTransactionAsync();
            }

            return Transaction;
        }

        private async void RollBack()
        {
            if (Transaction != null)
            {
                await Transaction.RollbackAsync();
            }
        }

        private async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                RollBack();
                throw new Exception(ex.Message);
            }
        }

        public async Task Commit()
        {
            if (Transaction != null)
            {
                await Transaction.CommitAsync();
                await Transaction.DisposeAsync();
                Transaction = null;
            }
        }

        public async Task SendChanges()
        {
            await Save();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
