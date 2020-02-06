using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.DAL.Repositories.Concrete
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly CommerceContext _dbContext;

        public ProductRepository(CommerceContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetTopProductInMonth(int month, int count)
        {
            //throw new NotImplementedException();
            return (from m in _dbContext.Products
                    orderby m.Rating descending
                    where m.CreateDate.Value.Month.Equals(month)
                    select m).Take(count);
        }
    }
}
