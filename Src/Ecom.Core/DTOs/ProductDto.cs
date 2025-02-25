using Ecom.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecom.Core.DTOs
{
    public class BaseProduct
    {
        [Required]
        public string name { get; set; } = null!;

        public string Description { get; set; } = null!;

        [Range(1,999,ErrorMessage = "Price Limited By {0} and {1}")]
        [RegularExpression(@"[0-9]*\.?[0-9]+" , ErrorMessage ="{0} Must be number")]
        public decimal price { get; set; }
    }


    public class ProductDto : BaseProduct
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;

        public string ProductPicture {  get; set; }

    }

    
    public class CreateProductDto : BaseProduct
    {
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; } = null!;

    }


    public class UpdateProductDto : BaseProduct
    {
       
        public int CategoryId { get; set; }

        public string? OldImage { get; set; }
        public IFormFile Image { get; set; } = null!;

    }

}
