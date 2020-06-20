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
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}
