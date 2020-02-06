using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApp.DAL;
using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private IRepository<Category> _repo;
        private IUnitOfWork _unitOfWork;
        private CommerceContext _context;
        public CategoryController(IRepository<Category> repo, IUnitOfWork unitOfWork, CommerceContext context )
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            var category =  _repo.GetAll();
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category, string name)
        {
            Category currentCategory = _unitOfWork.GetRepository<Category>().GetWhere(x => x.Name == name).FirstOrDefault();

            if(name == null)
            {
                ModelState.AddModelError("Name", "Can't be empty!");
            }
            else
            {
                if(currentCategory == null)
                {
                    _unitOfWork.GetRepository<Category>().Add(category);
                    _unitOfWork.Save();
                    return RedirectToAction(nameof(Index));                   
                }
                else
                {
                    ModelState.AddModelError("Name", "Can't be same!");
                }
            }
            return View();
            
        }

        //Something not true then back. not working properly. js/deletePopup.js has problem
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _unitOfWork.GetRepository<Category>().Remove(id);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    

        [HttpGet]
        public IActionResult Edit(int id)
        {
           var category = _unitOfWork.GetRepository<Category>().GetById(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            _unitOfWork.GetRepository<Category>().Update(category);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}