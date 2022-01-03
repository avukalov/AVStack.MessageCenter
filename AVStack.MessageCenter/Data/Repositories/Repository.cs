using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVStack.MessageCenter.Data.Entities;
using AVStack.MessageCenter.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AVStack.MessageCenter.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly McDbContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(McDbContext context)
        {
            _context = context;
            _entities = context.Set<TEntity>();
        }

        // TODO: Add filtering, sorting and paging
        public async Task<IEnumerable<TEntity>> FindAsync() => await _entities.AsNoTracking().ToListAsync();

        public async Task<TEntity> GetAsync(Guid id) => await _entities.FirstOrDefaultAsync(e => e.Id.Equals(id));

        public async Task<bool> InsertAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await _entities.AddAsync(entity);

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);

            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}