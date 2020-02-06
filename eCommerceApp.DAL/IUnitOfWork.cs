using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eCommerceApp.DAL
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class; 
        int Save();
        Task<int> SaveAsync();
    }
}
