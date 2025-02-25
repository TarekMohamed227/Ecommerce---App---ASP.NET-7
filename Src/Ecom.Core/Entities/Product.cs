using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities
{
    public class Product:BaseEntity<int>
    {
        public string name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal price { get; set; }
         
        public string ProductPicture { get; set; }

        //Navigation Property

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
