using System.Linq.Expressions;
using AutoMapper;
using Ecom.API.Errors;
using Ecom.API.Helper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }

        [HttpGet("Get-All-Products")]

        public async Task<ActionResult> Get([FromQuery]ProductParams? productParams)
        {
            //var Src = await _unitOfWork.ProductRepository.GetAllAsync(x => x.Category);

            var Src = await _unitOfWork.ProductRepository.GetAllAsync(productParams);

           
            if (Src is not null)
            {
                var result = _mapper.Map<IReadOnlyList<ProductDto>>(Src);
                var totalCount = result.Count();
                return Ok(new Pagination<ProductDto>(productParams.PageNumber, productParams.PageSize,totalCount,result));
            }

            return BadRequest("Not Found");
        }

        [HttpGet("Get-Product-By-Id/{id}")]

        public async Task<ActionResult> Get(int id)
        {
            var Src = await _unitOfWork.ProductRepository.GetByIdAsync(id, x => x.Category);

           

            if (Src is not null)
            {
                var result = _mapper.Map<ProductDto>(Src);
                return Ok(result);
            }
            else if (Src is null)
            {
                return NotFound(new BaseCommonResponse(404));
            }
            return BadRequest("Not Found");
        }

        [HttpPost("Add-New-product")]

        public async Task<ActionResult> Post([FromForm]CreateProductDto CreateproductDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _unitOfWork.ProductRepository.AddAsync(CreateproductDto);
                    return result? Ok(CreateproductDto):BadRequest(result);
                }
                return BadRequest(CreateproductDto);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update-Exiting-Product/{id}")]

        public async Task<ActionResult> Put(int id,[FromForm]UpdateProductDto UpdateproductDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _unitOfWork.ProductRepository.UpdateAsync( id, UpdateproductDto);
                    return result ? Ok(UpdateproductDto) : BadRequest(result);
                }
                return BadRequest(UpdateproductDto);
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete-Exiting-Product/{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var result = await _unitOfWork.ProductRepository.DeleteAsyncWithPicture(id);
                    return result ? Ok(result) : BadRequest(result);
                }
                return NotFound($"This Id {id} Not Found");
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
