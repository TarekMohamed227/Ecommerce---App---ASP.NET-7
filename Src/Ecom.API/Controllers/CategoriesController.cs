using AutoMapper;
using Ecom.Core.DTOs;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoriesController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("Get-All-Categories")]
        public async Task<ActionResult> Get()
        {
            var allCategories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (allCategories is not null)
            {
                var res = _mapper.Map<IReadOnlyList<Category>,IReadOnlyCollection<ListinigCategory>>(allCategories);
                //var res = allCategories.Select(X => new ListinigCategory

                //{
                //    Id = X.Id,
                //    Name = X.Name,
                //    Description = X.Description,
                //}).ToList();
                return Ok(res);
            }

                
            return BadRequest("Not Found");



        }

        [HttpGet("Get-Category-By-id/{id}")]

        public async Task<ActionResult> Get(int id)
        {
            var Category =await _unitOfWork.CategoryRepository.GetAsync(id);
           
             
            if (Category is not  null)
            {
                //var res = new ListinigCategory
                //{
                //    Id=Category.Id,
                //    Name = Category.Name,
                //    Description = Category.Description,
                //};
                return Ok(_mapper.Map<Category, ListinigCategory>(Category));
            }
                
                return BadRequest("Not Found");
            
        }


        [HttpPost("Add-New-Category")]

        public async Task<ActionResult> Post(CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var newCategory = new Category();
                    //{
                    //    //Name = categoryDto.Name,
                    //    //Description = categoryDto.Description,
                    //};

                    var res = _mapper.Map<Category>(categoryDto);
                    await _unitOfWork.CategoryRepository.AddAsync(res);
                return Ok(categoryDto);
                }
                return BadRequest(categoryDto);
            }
            catch (Exception ex) 
            {
            return BadRequest(ex.Message);
            }
            

         
        }
        [HttpPut("Update-Exiting-Category-By-Id")]
        public async Task<ActionResult> Put(UpdatingCategory updatingCategory)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var exitingCategory = await _unitOfWork.CategoryRepository.GetAsync(updatingCategory.Id);
                    if (exitingCategory is not null)
                    {
                        //exitingCategory.Name = categoryDto.Name;
                        //exitingCategory.Description = categoryDto.Description;
                        _mapper.Map(updatingCategory, exitingCategory);

                        await _unitOfWork.CategoryRepository.UpdateAsync(updatingCategory.Id, exitingCategory);
                        return Ok(updatingCategory);

                    }
                }
                return BadRequest($"Category Not found , Id [{updatingCategory.Id}] Incorrect");
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete-Category-By-Id/{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var exitingCategory = await _unitOfWork.CategoryRepository.GetAsync(id);
                if (exitingCategory is not null)
                {
                    await _unitOfWork.CategoryRepository.DeleteAsync(id);
                    return Ok($"this Category [{exitingCategory.Name}] is deleted successfully");

                }
                return BadRequest($"Category Not found , Id [{id}] Incorrect");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
