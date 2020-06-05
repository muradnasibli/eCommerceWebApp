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
    public class BrandStoreViewComponent : ViewComponent
    {
        private IRepository<Brand> _repo;
        public BrandStoreViewComponent(IRepository<Brand> repo)
        {
            _repo = repo;
        }

        public ViewViewComponentResult Invoke()
        {
            BrandListViewModel model = new BrandListViewModel
            {
                Brands = _repo.GetAll().ToList()
            };
            return View(model);
        }
    }
}
