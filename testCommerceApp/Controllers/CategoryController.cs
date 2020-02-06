using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApp.DAL;
using eCommerceApp.DAL.Repositories.Abstact;
using eCommerceApp.Domains;
using Microsoft.AspNetCore.Mvc;

namespace testCommerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CommerceContext _dbTest;
        private readonly UnitOfWork _uoW;
        private IRepository<Category> _repo;
        public CategoryController(IUnitOfWork uoW, IRepository<Category> repo, CommerceContext dbTest)
        {
            _uoW = uoW as UnitOfWork;
            _repo = repo;
            _dbTest = dbTest;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _repo.GetAll();
            return View(categories);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            _repo.Add(category);
            _uoW.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _uoW.GetRepository<Category>().Remove(id);
            _uoW.Save();
            return View();
        }
    }
}