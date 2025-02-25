using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _Context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        //public UnitOfWork(IMapper mapper)
        //{
        //    _mapper = mapper;
        //}
        public ICategoryRepository CategoryRepository { get;  }

        public IProductRepository ProductRepository { get;  }

        public UnitOfWork(ApplicationDbContext context,IFileProvider fileProvider,IMapper mapper)
        {
            _Context=context;
            _fileProvider = fileProvider;
            _mapper = mapper;
            CategoryRepository = new CategoryRepository(_Context);
            ProductRepository = new ProductRepository(_Context, _fileProvider,_mapper);
        }

       
    }
}
