using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Web.Business.Abstract;
using eCommerceApp.Web.Helpers;
using eCommerceApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApp.Web.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository _productRepo;
        private ICartService _cartService;
        private ICartSessionHelper _cartSessionHelper;
        public CartController(IProductRepository productRepo, ICartService cartService, ICartSessionHelper cartSessionHelper)
        {
            _productRepo = productRepo;
            _cartService = cartService;
            _cartSessionHelper = cartSessionHelper;
        }

        public IActionResult AddToCart(int productId)
        {
            var product = _productRepo.GetById(productId);
            var cart = _cartSessionHelper.GetCart("cart");

            _cartService.AddToCart(cart, product);

            _cartSessionHelper.SetCart("cart", cart);

            return RedirectToAction("Index", "Home"); 
        }

        public  IActionResult RemoveFromCart(int productId)
        {
            var product = _productRepo.GetById(productId);
            var cart = _cartSessionHelper.GetCart("cart");

            _cartService.RemoveFromCart(cart, productId);
            _cartSessionHelper.SetCart("cart", cart);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Checkout()
        {
            var model = new CartListViewModel
            {
                Cart = _cartSessionHelper.GetCart("cart")
            };
            return View(model);
        }
    }
}
