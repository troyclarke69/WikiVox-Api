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
    public class GenreController : ControllerBase
    {
        private readonly GenreService _genreService;

        public GenreController(GenreService service)
        {
            _genreService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetAll()
        {
            var Genres = await _genreService.GetAllAsync();
            return Ok(Genres);
        }

        [HttpGet("{id:length(24)}", Name = "GetGenre")]
        public async Task<ActionResult<Genre>> GetById(string id)
        {
            var Genre = await _genreService.GetByIdAsync(id);
            if (Genre == null)
            {
                return NotFound();
            }
           
            return Ok(Genre);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Genre Genre)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _genreService.CreateAsync(Genre);
            return Ok(Genre);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Genre updatedGenre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedGenre = await _genreService.GetByIdAsync(id);
            if (queriedGenre == null)
            {
                return NotFound();
            }
            await _genreService.UpdateAsync(id, updatedGenre);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Genre = await _genreService.GetByIdAsync(id);
            if (Genre == null)
            {
                return NotFound();
            }
            await _genreService.DeleteAsync(id);
            return NoContent();
        }
    }
}
