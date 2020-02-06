using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.DAL.Repositories.Concrete;
using eCommerceApp.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eCommerceApp.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private CommerceContext _dbContext;


        public UnitOfWork(CommerceContext dbContext)
        {
            _dbContext = dbContext;
        }


        public int Save()
        {
            return _dbContext.SaveChanges();
        }
        
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(_dbContext);
        }

        public async Task<int> SaveAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
