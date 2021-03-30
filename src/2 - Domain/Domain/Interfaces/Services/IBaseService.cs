using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ToDo.Domain.Interfaces.Services
{
    public interface IBaseService<Entity> where Entity : BaseEntity
    {
        Task Save(Entity entity);
        Task Delete(int id);
        Task<IEnumerable<Entity>> GetAll();
        Task<Entity> GetById(int id);
        Task<IEnumerable<Entity>> GetByExpression(Expression<Func<Entity, bool>> filter = null);
    }
}
