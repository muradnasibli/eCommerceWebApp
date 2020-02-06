using eCommerceApp.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApp.Web.ViewModels
{
    public class QuickViewViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Colour { get; set; }

        public string Size { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? DiscountedPrice { get; set; }

        public int Quantity { get; set; }

        [StringLength(maximumLength: 250)]
        public string Description { get; set; }

        public string DetailInformation { get; set; }

        public string ImageUrl { get; set; }

        public bool InStock { get; set; }

        public int? Rating { get; set; }

        //Relationship 
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
