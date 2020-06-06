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

        //public IActionResult Index()
        //{
        //    var products = new ProductListViewModel
        //    {
        //        Products = _repo.GetAll().ToList()
        //    };        
        //    return View(products);
        //}

        public IActionResult Index(int category)
        {
            var model = new ProductListViewModel
            {
                Products = category > 0 ? _repo.GetAll().Where(x => x.CategoryId == category).ToList() : _repo.GetAll().ToList()
            };

            return View(model);
        }
    }
}
