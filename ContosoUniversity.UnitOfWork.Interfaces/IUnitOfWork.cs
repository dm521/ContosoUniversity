using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.UnitOfWork.Interfaces
{
    public interface IUnitOfWork
    {
        void Add<TEntity>(TEntity entity) where TEntity : class;

        void Dirty<TEntity>(TEntity entity) where TEntity : class;

        void Clear<TEntity>(TEntity entity) where TEntity : class;

        void Remove<TEntity>(TEntity entity) where TEntity : class;

        Task<bool> CommitAsync();

        void Rollback();
    }
}
