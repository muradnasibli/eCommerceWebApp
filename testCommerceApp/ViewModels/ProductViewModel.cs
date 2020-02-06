using eCommerceApp.Domains;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testCommerceApp.ViewModels
{
    public class ProductViewModel : BaseEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Colour { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountedPrice { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public string DetailInformation { get; set; }

        public bool InStock { get; set; }

        public int? Rating { get; set; }

        public IFormFile Photo { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
