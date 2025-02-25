using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecom.Core.Sharing;
using Ecom.Core.DTOs;
using Ecom.Core.Entities;
using Ecom.Core.Sharing;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<bool> AddAsync(CreateProductDto Dto);
        Task<bool> UpdateAsync(int id, UpdateProductDto Dto);
        Task<bool> DeleteAsyncWithPicture(int Id);
        Task<IEnumerable<ProductDto>> GetAllAsync(ProductParams productParams);
    }
}
