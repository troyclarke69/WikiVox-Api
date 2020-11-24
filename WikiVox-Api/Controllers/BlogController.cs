using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService service)
        {
            _blogService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blogg>>> GetAll()
        {
            var Blogg = await _blogService.GetAllAsync();
            return Ok(Blogg);
        }

        [HttpGet("{id:length(24)}", Name = "GetBlog")]
        public async Task<ActionResult<Blogg>> GetById(string id)
        {
            var Blogg = await _blogService.GetByIdAsync(id);
            if (Blogg == null)
            {
                return NotFound();
            }
            return Ok(Blogg);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blogg Blogg)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _blogService.CreateAsync(Blogg);
            return Ok(Blogg);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Blogg updatedBlog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedBlog = await _blogService.GetByIdAsync(id);
            if (queriedBlog == null)
            {
                return NotFound();
            }
            await _blogService.UpdateAsync(id, updatedBlog);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Blogg = await _blogService.GetByIdAsync(id);
            if (Blogg == null)
            {
                return NotFound();
            }
            await _blogService.DeleteAsync(id);
            return NoContent();
        }
    }
}
