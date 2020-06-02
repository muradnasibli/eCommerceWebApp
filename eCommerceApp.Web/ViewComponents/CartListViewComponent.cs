using eCommerceApp.DAL;
using eCommerceApp.Domains;
using eCommerceApp.Web.Business.Abstract;
using eCommerceApp.Web.Helpers;
using eCommerceApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApp.Web.ViewComponents
{
    public class CartListViewComponent : ViewComponent
    {
        private ICartService _cartService;
        private ICartSessionHelper _cartSessionHelper;
        private IUnitOfWork _unitOfWork;

        public CartListViewComponent(ICartService cartService, ICartSessionHelper cartSessionHelper, IUnitOfWork unitOfWork)
        {
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
            _unitOfWork = unitOfWork;
        }

        public ViewViewComponentResult Invoke()
        {
            var model = new CartListViewModel
            {
                Cart = _cartSessionHelper.GetCart("cart")
            };
            return View(model);
        }

    }
}
