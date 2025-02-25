using AutoMapper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities;

namespace Ecom.API.MappingProfile
{
    public class MappingCategory:Profile
    {

        public MappingCategory() 
        {
            CreateMap<CategoryDto,Category>().ReverseMap();
            CreateMap<ListinigCategory, Category>().ReverseMap();
            CreateMap<UpdatingCategory, Category>().ReverseMap();
            
        }
    }
}
