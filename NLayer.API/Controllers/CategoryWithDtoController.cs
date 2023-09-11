using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryWithDtoController : CustomBaseController
    {
        private readonly IServiceWithDto<Category, CategoryDTO> _categoryServiceWithDto;

        public CategoryWithDtoController(IServiceWithDto<Category, CategoryDTO> categoryService)
        {
            _categoryServiceWithDto = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return CreateActionResult(await _categoryServiceWithDto.GetAllAsync());
        }


        [HttpPost]
        public async Task<IActionResult> Save(CategoryDTO categoryDto)
        {
            return CreateActionResult(await _categoryServiceWithDto.AddAsync(categoryDto));
        }
    }
}
