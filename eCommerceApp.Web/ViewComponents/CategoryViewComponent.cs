﻿using eCommerceApp.DAL;
using eCommerceApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApp.Web.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private CommerceContext _dbContext;

        public CategoryViewComponent(CommerceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            SelectListItemViewModel selectList = new SelectListItemViewModel
            {
                SelectListCategory = await _dbContext.Categories.Select(d => new SelectListItem
                {
                    Text = d.Name,
                    Value = d.Id.ToString()
                }).ToListAsync(),
            };
            return View(selectList);
        }
    }
}
