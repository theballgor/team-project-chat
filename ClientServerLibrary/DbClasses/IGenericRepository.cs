using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ClientServerLibrary.DbClasses
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Add(TEntity item);
        TEntity FindById(object id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> ExecWithStoreProcedure(string query, params object[] parameters);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
