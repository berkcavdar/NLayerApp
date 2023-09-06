﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        //GET api/categories/getsinglecategorybyidwithproducts/id
        [HttpGet("[action]/{categoryid}")]
        public async Task<IActionResult> GetSingleCategoryByIdWithProducts(int categoryId)
        {
           return CreateActionResult(await _categoryService.GetSingleCategoryByIdWithProducts(categoryId));
        }
    }
}
