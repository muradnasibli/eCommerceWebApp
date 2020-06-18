using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Domains;
using eCommerceApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApp.Web.Controllers
{
    public class StoreController : Controller
    {
        private IRepository<Product> _repo;
        public StoreController(IRepository<Product> repo)
        {
            _repo = repo;
        }

        public IActionResult Index(int category, int brand, string name)
        {
            var model = new ProductListViewModel
            {
                Products = category > 0 ? _repo.GetAll().Where(x => x.CategoryId == category).ToList() :
                            brand > 0 ? _repo.GetAll().Where(x=> x.BrandId == brand).ToList() :
                            name != null ? _repo.GetWhere(x=> x.Name.Contains(name)).ToList() //search by name
                            : _repo.GetAll().ToList()
            };

            return View(model);
            
        }
        
    }
}
