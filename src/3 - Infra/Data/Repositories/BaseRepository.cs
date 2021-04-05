using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces.Repository;

namespace Data.Repositories
{
    public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : BaseEntity
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly DbSet<Entity> _dbSet;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbSet = unitOfWork.GetContext().Set<Entity>();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _dbSet.Remove(entity);

            await SaveChanges();
        }

        public async Task<IEnumerable<Entity>> GetAll()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IList<Entity>> GetByExpression(Expression<Func<Entity, bool>> filter = null)
        {
            if (filter != null)
            {
                return await _dbSet.Where(filter).ToListAsync();
            }
            else
            {
                return await _dbSet.ToListAsync();
            }
        }

        public async Task<Entity> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Save(Entity entity)
        {
            await _unitOfWork.InitTransacao();

            if (entity.Id == 0)
            {
                await _dbSet.AddAsync(entity);
            }
            else
            {
                await Update(entity);
            }

            await SaveChanges();
        }

        public async Task Update(Entity entity)
        {
            _dbSet.Update(entity);
            await SaveChanges();
        }

        private async Task SaveChanges()
        {
            await _unitOfWork.SendChanges();
        }
    }
}
