using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(new Response<IEnumerable<ProductDto>>(products, "Products retrieved successfully", true));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
                return NotFound();

            return Ok(new Response<ProductDto>(product, "Product retrieved successfully", true));
        }

        [HttpPost]
        public async Task<ActionResult<Response<ProductDto>>> Create(CreateProductDto createProductDto)
        {
            var product = await _productService.CreateAsync(createProductDto);

            var response = new Response<ProductDto>(
                product,
                "Product created successfully",
                true
            );

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, response);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ProductDto>> Update(int id, UpdateProductDto updateProductDto)
        {
            var product = await _productService.UpdateAsync(id, updateProductDto);
            if (product == null)
                return NotFound();

            return Ok(new Response<ProductDto>(product, "Product updated successfully", true));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 