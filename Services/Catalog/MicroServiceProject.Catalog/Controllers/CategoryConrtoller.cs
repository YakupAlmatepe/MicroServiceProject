﻿using MicroServiceProject.Catalog.Dtos;
using MicroServiceProject.Catalog.Services.Abstract;
using MicroServiceProject.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroServiceProject.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryConrtoller : CustomeBaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryConrtoller(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return CreateActionResultInstance(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(string id)
        {
            var categories = await _categoryService.GetByIDAsync(id);
            return CreateActionResultInstance(categories);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            var categories = await _categoryService.DeleteAsync(id);
            return CreateActionResultInstance(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto createCategoryDto)
        {
            var categories = await _categoryService.CreateAsync(createCategoryDto);
            return CreateActionResultInstance(categories);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {
            var categories = await _categoryService.UpdateAsync(updateCategoryDto);
            return CreateActionResultInstance(categories);
        }
    }
}
