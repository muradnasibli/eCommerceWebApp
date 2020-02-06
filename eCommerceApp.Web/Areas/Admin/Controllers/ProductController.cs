using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApp.DAL;
using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Domains;
using eCommerceApp.Web.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private IProductRepository _productRepo;
        private IUnitOfWork _unitOfWork;
        private IHostingEnvironment _hostingEnvironment;

        public ProductController(IProductRepository productRepo, IUnitOfWork unitOfWork, IHostingEnvironment hostingEnvironment)
        {
            _productRepo = productRepo;
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var product = _unitOfWork.GetRepository<Product>().Include(x => x.Category, x => x.Brand).ToList();
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                CreateDate = DateTime.Now,
                ImageUrl = uniqueFileName,
                IsActive = model.IsActive,
            };
            
            _unitOfWork.GetRepository<Product>().Add(product);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _unitOfWork.GetRepository<Product>().GetById(id);

            ProductEditViewModel model = new ProductEditViewModel
            {
                Id = product.Id,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductEditViewModel model)
        {
            var product = _unitOfWork.GetRepository<Product>().GetById(model.Id);

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
            product.UpdateDate = DateTime.Now;

            if (model.Photo != null)
            {
                if (model.ExistingPath != null)
                {
                    var stringPath = Path.Combine(_hostingEnvironment.WebRootPath, "productImages", model.ExistingPath);
                    System.IO.File.Delete(stringPath);
                }
                product.ImageUrl = ProcessUploadFile(model);
            }

            _unitOfWork.GetRepository<Product>().Update(product);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _unitOfWork.GetRepository<Product>().Remove(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _unitOfWork.GetRepository<Product>().Include(x => x.Category, x => x.Brand)
                                                                            .Where(x => x.Id == id).FirstOrDefault();
            return View(product);

        }

        //Photo Upload method with Guid + Model.Photo.FileName;
        private string ProcessUploadFile(ProductViewModel model)
        {
            string uniqueFileName = null;
            if(model.Photo != null)
            {
                string uploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "productImages");
                uniqueFileName = Guid.NewGuid().ToString() + "-" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}