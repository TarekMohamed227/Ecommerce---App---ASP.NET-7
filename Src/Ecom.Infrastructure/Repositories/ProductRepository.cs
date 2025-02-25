using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
        {
            _fileProvider = fileProvider;
            _mapper = mapper;

            _context = context;
        }

        public async Task<bool> AddAsync(CreateProductDto Dto)
        {
            var Src = "";
            if (Dto.Image is not null)
            {

                var root = "wwwroot/images/";
                var ProductName = $"{Guid.NewGuid()}" + Dto.Image.FileName;
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory("wwwroot" + root);
                }
                Src = root + ProductName;
                //var PicInfo = _fileProvider.GetFileInfo(Src);
                //var RootPath = PicInfo.PhysicalPath;

                using (var FileStream = new FileStream(Src, FileMode.Create))
                {

                    await Dto.Image.CopyToAsync(FileStream);
                }



            }
            //Create New Product
            var res = _mapper.Map<Product>(Dto);
            res.ProductPicture = Src;
            await _context.Products.AddAsync(res);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int Id, UpdateProductDto Dto)
        {

            //var res = _mapper.Map<Product>(Dto);
            //res.Id = Id;
            //res.ProductPicture = "sss";
            //_context.Products.Update(res);
            //await _context.SaveChangesAsync();

            var currentProduct = await _context.Products.FindAsync(Id);
            if (currentProduct is not null)
            {
                var Src = "";
                if (Dto.Image is not null)
                {

                    var root = "wwwroot/images/";
                    var ProductName = $"{Guid.NewGuid()}" + Dto.Image.FileName;
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory( root);
                    }
                    Src = root + ProductName;
                    //var PicInfo = _fileProvider.GetFileInfo(Src);
                    //var RootPath = PicInfo.PhysicalPath;

                    using (var FileStream = new FileStream(Src, FileMode.Create))
                    {

                        await Dto.Image.CopyToAsync(FileStream);
                    }



                }
                //Remove Old Picture
                if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                {
                    //Delete Old Picture

                    //var PicInfo = _fileProvider.GetFileInfo(currentProduct.ProductPicture);
                    //var RootPath = PicInfo.PhysicalPath;
                    System.IO.File.Delete(currentProduct.ProductPicture);
                }

                //Update New Product
                currentProduct.ProductPicture = Src;
                currentProduct.price = Dto.price;
                currentProduct.name = Dto.name;
                currentProduct.Description = Dto.Description;
                currentProduct.CategoryId = Dto.CategoryId;

                _context.Products.Update(currentProduct);
                await _context.SaveChangesAsync();


                return true;
            }
            return true;

        }

        public async Task<bool> DeleteAsyncWithPicture(int Id)
        {
            var currentProduct = await _context.Products.FindAsync(Id);
            if (currentProduct is not null)
            {
                //Remove Old Picture
                if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                {
                    //Delete Old Picture

                    var PicInfo = _fileProvider.GetFileInfo(currentProduct.ProductPicture);
                    var RootPath = PicInfo.PhysicalPath;
                    System.IO.File.Delete(RootPath);
                }

                _context.Products.Remove(currentProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;


        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync( ProductParams productParams)
        {
            var query = await _context.Products.Include(x=>x.Category).AsNoTracking().ToListAsync();


            //Serach by Name

            if(!string.IsNullOrEmpty(productParams.Search))
            {
                query= query.Where(x=>x.name.ToLower().Contains(productParams.Search)).ToList();
            }

            //Search by Category

            if(productParams.CategoryId.HasValue)
            
                query = query.Where(x => x.CategoryId == productParams.CategoryId.Value).ToList();
            
          
 
            //Sorting

            if (!string.IsNullOrEmpty(productParams.sort))
            {
                switch (productParams.sort)
                {
                    case "PriceAsync":
                        query = query.OrderBy(x => x.price).ToList();
                        break;
                    case "PriceDesc":
                        query=query.OrderByDescending(x => x.price).ToList();
                        break;

                    default:
                        query=query.OrderBy(x=>x.name).ToList();
                        break;
                }
                
            }


            //Paging

            query = query.Skip((productParams.PageSize) * (productParams.PageNumber - 1)).Take(productParams.PageSize).ToList();

            var result =_mapper.Map<List<ProductDto>>(query);
            return result;
        }
    }
}
