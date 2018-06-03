using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tal.Mdm.Base;

namespace Piffnium.Web.Infrastructure.Repositories
{
    public abstract class EfRepository<TContext, TEntity> : IRepository<TEntity> where TContext : DbContext where TEntity : class
    {
        protected readonly TContext dbContext;

        protected EfRepository(TContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

            var entityType = this.dbContext.Model.FindEntityType(typeof(TEntity));
        }

        //TODO  First のほうが良い？？
        public Task<TEntity> SingleAsync(IQuerySpec<TEntity> spec)
        {
            return GetQueryable(spec).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> ListAsync(IQuerySpec<TEntity> spec)
        {
            var query = spec.GetIncludes()
                .Aggregate(this.dbContext.Set<TEntity>().AsQueryable(),
                    (current, include) => current.Include(include));

            query = spec.GetCriterias().Aggregate(query,
                    (current, criteria) => current.Where(criteria));

            return await GetQueryable(spec).ToListAsync().ConfigureAwait(false);
        }

        private IQueryable<TEntity> GetQueryable(IQuerySpec<TEntity> spec)
        {
            var query = spec.GetIncludes()
                .Aggregate(this.dbContext.Set<TEntity>().AsQueryable(),
                    (current, include) => current.Include(include));

            query = spec.GetCriterias().Aggregate(query,
                    (current, criteria) => current.Where(criteria));

            return query.AsNoTracking();
        }
    }
}
