using eCommerceApp.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Repositories.Abstact
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetTopProductInMonth(int month, int count);
    }
}
