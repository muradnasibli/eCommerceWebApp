using eCommerceApp.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApp.Web.ViewModels
{
    public class BrandListViewModel
    {
        public List<Brand> Brands { get; set; }
        public int CurrentBrand { get; set; }
    }
}
