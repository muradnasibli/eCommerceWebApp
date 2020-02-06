using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Repositories.Abstact
{
    public interface IRepository<TEntity> where TEntity:class
    {
        TEntity GetById(int id);

        IEnumerable<TEntity> GetAll();

        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(int id);

        void Delete(TEntity entity);


    }
}
