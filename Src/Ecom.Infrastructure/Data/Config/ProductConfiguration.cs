using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecom.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.name).HasMaxLength(30);
            builder.Property(x => x.price).HasColumnType("decimal(18, 2)");

            //seed

            builder.HasData(
                new Product { Id=1,name="Product_1",Description="P1", price=2000,CategoryId=1,ProductPicture="https://" },
                 new Product { Id = 2, name = "Product_2", Description = "P2", price = 3000, CategoryId = 2, ProductPicture = "https://" },
                 new Product { Id = 3, name = "Product_3", Description = "P3", price = 4000, CategoryId = 1, ProductPicture = "https://" }
                );

        }
    }
}
