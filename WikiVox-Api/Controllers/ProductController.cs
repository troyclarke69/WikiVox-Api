using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService service)
        {
            _productService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] ProductParams productParams)
        {
            var Products = await _productService.GetAllAsync(productParams);
            var meta = new
            {
                Products.TotalCount,
                Products.PageSize,
                Products.CurrentPage,
                Products.TotalPages,
                Products.HasNext,
                Products.HasPrevious
            };

            Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(meta));
            return Ok(Products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var Product = await _productService.GetByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(Product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product Product)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _productService.CreateAsync(Product);
            return Ok(Product);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Product updatedProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedProduct = await _productService.GetByIdAsync(id);
            if (queriedProduct == null)
            {
                return NotFound();
            }
            await _productService.UpdateAsync(id, updatedProduct);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Product = await _productService.GetByIdAsync(id);
            if (Product == null)
            {
                return NotFound();
            }
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}
