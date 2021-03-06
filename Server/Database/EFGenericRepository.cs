using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Server.Database
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        public DbContext _context;
        DbSet<TEntity> _dbSet;

        public EFGenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
            _context.Configuration.ProxyCreationEnabled = false;
        }

        public IEnumerable<TEntity> GetAll() { return _dbSet.ToList(); }

        public IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate) { return _dbSet.Where(predicate).ToList(); }

        public TEntity FindById(object id) { return _dbSet.Find(id); }

        public void Add(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }


        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties) { return Include(includeProperties).ToList(); }

        public IEnumerable<TEntity> GetWithInclude(Func<TEntity, bool> predicate,
         params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = Include(includeProperties);
            return query.Where(predicate).ToList();
        }

        public IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters)
        {
            return _context.Database.SqlQuery<TEntity>(query, parameters);
        }

        public int ExecScalar(string query, params object[] parameters)
        {
            return _context.Database.SqlQuery<int>(query, parameters).FirstOrDefault();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;
            return includeProperties
            .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }

    }
}
