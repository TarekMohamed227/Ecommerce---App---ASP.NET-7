using AutoMapper;
using Ecom.API.Helper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities;

namespace Ecom.API.MappingProfile
{
    public class MappingProduct:Profile
    {

        public MappingProduct()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d=>d.CategoryName, o=> o.MapFrom(s=>s.Category.Name))
                .ForMember(d=>d.ProductPicture,o=>o.MapFrom<ProductUrlResolver>());
            CreateMap<CreateProductDto, Product >().ReverseMap();
            CreateMap<UpdateProductDto,Product > ().ReverseMap();
        }
    }
}
