using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces.Repository;
using ToDo.Domain.Interfaces.Services;

namespace Data.Services
{
    public class BaseService<Entity> : IBaseService<Entity> where Entity : BaseEntity
    {
        protected readonly IBaseRepository<Entity> _repository;

        public BaseService(IBaseRepository<Entity> repository)
        {
            _repository = repository;
        }

        public async Task Save(Entity entity)
        {
            await _repository.Save(entity);
        }

        public async Task Delete(int id)
        {
            await _repository.Delete(id);
        }

        public virtual async Task<IEnumerable<Entity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public virtual async Task<Entity> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public virtual async Task<IEnumerable<Entity>> GetByExpression(Expression<Func<Entity, bool>> filter = null)
        {
            return await _repository.GetByExpression(filter);
        }
    }
}
