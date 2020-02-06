using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApp.DAL;
using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Domains;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Brand> _repo;
        public BrandController(IUnitOfWork unitOfWork, IRepository<Brand> repo)
        {
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
           var brand =  _unitOfWork.GetRepository<Brand>().GetAll();
           return View(brand);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand model, string name)
        {
            
            Brand brand = new Brand
            {
                Name = model.Name,
                CreateDate = DateTime.Now,
                DeleteDate = model.DeleteDate,
                UpdateDate = model.DeleteDate,
                IsActive = model.IsActive
            };

            //Brand currentBrand = _unitOfWork.GetRepository<Brand>().GetAll().Where(x => x.Name == name).FirstOrDefault();
            Brand currentBrand = _unitOfWork.GetRepository<Brand>().GetWhere(x => x.Name == name).FirstOrDefault();

            if(name == null)
            {
                ModelState.AddModelError("Name", "Can't be empty!");
            }
            else
                if(currentBrand == null)
                {
                    _unitOfWork.GetRepository<Brand>().Add(brand);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Can't be same!");
                }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
           var brand = _unitOfWork.GetRepository<Brand>().GetById(id);
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Brand brand = new Brand
            {
                Id = model.Id,
                Name = model.Name,
                CreateDate = model.CreateDate,
                DeleteDate = model.DeleteDate,
                UpdateDate = DateTime.Now,
                IsActive = model.IsActive
            };

            _unitOfWork.GetRepository<Brand>().Update(brand);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost, ActionName("Delete")]  
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _unitOfWork.GetRepository<Brand>().Remove(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}