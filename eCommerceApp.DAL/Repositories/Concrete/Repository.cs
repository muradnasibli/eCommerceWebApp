using eCommerceApp.DAL.Repositories.Abstact;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Repositories.Concrete
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly CommerceContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(CommerceContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            _dbSet.Add(entity); 
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public TEntity GetById(int id)
        {
            return _dbSet.Find(id);
        }

        

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> set = _dbSet;

            foreach (var includeExpression in includes)
            {
                set = set.Include(includeExpression);
            }
            return set;
        }
        
        public void Remove(int id)
        {
            _dbSet.Remove(GetById(id));
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Update(entity).State = EntityState.Modified;
        }
    }
}
