using eCommerceApp.Domains;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApp.Web.ViewModels
{
    public class SelectListItemViewModel
    {
        public List<SelectListItem> SelectListCategory { get; set; } 
        public Category Category { get; set; }

        public List<SelectListItem> SelectListBrand { get; set; }
        public Brand Brand { get; set; }
    }
}
