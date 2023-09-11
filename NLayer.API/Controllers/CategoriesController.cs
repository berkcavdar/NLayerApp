using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetALl()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories.ToList());
            return CreateActionResult(CustomResponseDTO<List<CategoryDTO>>.Success(StatusCodes.Status200OK, categoriesDto));
        }



        //GET api/categories/getsinglecategorybyidwithproducts/id
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int id)
        {
           return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProducts(id));
        }
    }
}
