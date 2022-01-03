using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVStack.MessageCenter.Data.Entities;

namespace AVStack.MessageCenter.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> FindAsync();
        Task<TEntity> GetAsync(Guid id);
        Task<bool> InsertAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}