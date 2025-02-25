using System.ComponentModel.DataAnnotations;

namespace Ecom.Core.DTOs
{
    public class CategoryDto
    {
        [Required]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }

    public class ListinigCategory : CategoryDto
    {

        [Required]
        public int Id { get; set; }
    }
    public class UpdatingCategory : CategoryDto
    {

        [Required]
        public int Id { get; set; }
    }
}
