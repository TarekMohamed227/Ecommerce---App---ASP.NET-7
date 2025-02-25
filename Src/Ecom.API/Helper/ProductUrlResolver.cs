﻿using AutoMapper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities;

namespace Ecom.API.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;
        public ProductUrlResolver( IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.ProductPicture))
                {
                return _configuration["ApiURl"]+ source.ProductPicture;
            }
            return null;
        }
    }
}
