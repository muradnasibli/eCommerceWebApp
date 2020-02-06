using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApp.DAL;
using eCommerceApp.Domains;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Web.Controllers
{
    public class AjaxController : Controller
    {
        private IUnitOfWork _unitOfWork;
        public AjaxController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult LoadProducts(int? categoryId)
        {
            var data = _unitOfWork.GetRepository<Product>().GetWhere(x => x.CategoryId == categoryId).ToList();

            return PartialView("_LoadProductsPV",data);
        }
    }
}