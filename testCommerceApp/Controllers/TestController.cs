using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApp.DAL;
using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.DAL.Repositories.Concrete;
using eCommerceApp.Domains;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using testCommerceApp.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace testCommerceApp.Controllers
{
    public class TestController : Controller
    {
        private readonly IUnitOfWork _uoW;
        private IRepository<Product> _productRepo;
        private IProductRepository _repoProduct;
        private IHostingEnvironment hostingEnvironment;

        public TestController(IUnitOfWork uow, 
                                  IRepository<Product> productRepo,
                                     IProductRepository repoProduct, 
                                        IHostingEnvironment hostingEnvironment)
        {
            _uoW = uow;
            _productRepo = productRepo;
            _repoProduct = repoProduct;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index()
        {
            var listProducts =  _productRepo.GetAll();
            return View(listProducts);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductViewModel model)
        {
            string uniqueFileName = ProcessUploadFile(model);

            Product product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Colour = model.Colour,
                Size = model.Size,
                Price = model.Price,
                DiscountedPrice = model.DiscountedPrice,
                Quantity = model.Quantity,
                Description = model.Description,
                DetailInformation = model.DetailInformation,
                CategoryId = model.CategoryId,
                BrandId = model.BrandId,
                InStock = model.InStock,
                CreateDate = model.CreateDate,
                ImageUrl = uniqueFileName,
                IsActive = model.IsActive,
                
            };

            ////_repoProduct.Add(product);
            _uoW.GetRepository<Product>().Add(product);
            _uoW.Save();

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productRepo.GetById(id);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductEditViewModel model)
        {
            var product = _productRepo.GetById(model.Id);

            product.Name = model.Name;
            product.Colour = model.Colour;
            product.Size = model.Size;
            product.Price = model.Price;
            product.DiscountedPrice = model.DiscountedPrice;
            product.Quantity = model.Quantity;
            product.Description = model.Description;
            product.DetailInformation = model.DetailInformation;
            product.InStock = model.InStock;
            product.Rating = model.Rating;
            product.CategoryId = model.CategoryId;
            product.BrandId = model.BrandId;

            if ( model.Photo != null)
            {
                if(model.ExistingPath != null)
                {
                    var stringPath = Path.Combine(hostingEnvironment.WebRootPath, "images", model.ExistingPath);
                    System.IO.File.Delete(stringPath);
                }
                product.ImageUrl = ProcessUploadFile(model);
            }

            _productRepo.Update(product);
            _uoW.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _productRepo.Remove(id);
            _uoW.Save();
            return RedirectToAction(nameof(Index));
        }

        private string ProcessUploadFile(ProductViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}