using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoUniversity.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace ContosoUniversity.UnitOfWork.Persistence
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly IDbContext _dbContext;

        public EFUnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Clear<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Unchanged;
        }

        public async Task<bool> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public void Dirty<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
