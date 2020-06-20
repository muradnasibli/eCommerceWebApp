using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApp.DAL;
using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Domains;
using eCommerceApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Web.Controllers
{
    public class ProductViewController : Controller
    {
        private IProductRepository _productRepo;
        private IUnitOfWork _unitOfWork;
       

        public ProductViewController(IProductRepository productRepo, IUnitOfWork unitOfWork)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            
        }

        
        public IActionResult Product(int id)
        {
            var product = _unitOfWork.GetRepository<Product>().Include(x => x.Category, x=> x.Brand)
                                                                            .Where(x => x.Id == id).FirstOrDefault();

            ProductEditViewModel model = new ProductEditViewModel()
            {
                Id = product.Id,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Category = product.Category,
                Brand = product.Brand,
                Colour = product.Colour,
                CreateDate = product.CreateDate,
                Description = product.Description,
                DetailInformation = product.DetailInformation,
                DiscountedPrice = product.DiscountedPrice,
                InStock = product.InStock,
                ExistingPath = product.ImageUrl,
                IsActive = product.IsActive,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
                Rating = product.Rating,
                Size = product.Size
            };
            return View(model);
        }
    }
}