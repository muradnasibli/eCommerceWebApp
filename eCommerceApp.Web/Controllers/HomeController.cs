using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using eCommerceApp.DAL;
using eCommerceApp.Domains;
using eCommerceApp.Web.ViewModels;

namespace eCommerceApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork _unitOfWork;    
        

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int id)
        {
            var category = _unitOfWork.GetRepository<Category>().GetAll().ToList();
            var product = _unitOfWork.GetRepository<Product>().Include(x => x.Category, x => x.Brand).ToList();

            IndexViewModel viewModel = new IndexViewModel
            {
                Categories = category,
                Products = product
            };

            var productFilter = _unitOfWork.GetRepository<Product>().GetWhere(x => x.Id == id).ToList();
            
            return View(viewModel);
        }

        #region Filter for Index View but didn't work
        public IActionResult List()
        {
            return View();
        }

        [HttpPost]
        public IActionResult List(int categoryId)
        {
            return View("Index", ProductFilter(categoryId));
        }

        //Product change to category
        public IQueryable<Product> ProductFilter(int id)
        {
            return _unitOfWork.GetRepository<Product>().Include(x => x.Category).Where(x => x.Id == id).OrderBy(x => x.Name);
        }
        #endregion

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
