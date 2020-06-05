using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Domains;
using eCommerceApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApp.Web.ViewComponents
{
    public class CategoryStoreViewComponent : ViewComponent
    {
        private IRepository<Category> _repo;
        public CategoryStoreViewComponent(IRepository<Category> repo)
        {
            _repo = repo;
        }
        
        public ViewViewComponentResult Invoke()
        {
            CategoryListViewModel model = new CategoryListViewModel
            {
                Categories = _repo.GetAll().ToList(),
                CurrentCategory = Convert.ToInt32(HttpContext.Request.Query["category"])
            };
            return View(model);
        }
    }
}
