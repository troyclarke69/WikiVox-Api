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
    public class SongController : ControllerBase
    {
        private readonly SongService _songService;

        public SongController(SongService service)
        {
            _songService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetAll()
        {
            var Songs = await _songService.GetAllAsync();
            return Ok(Songs);
        }

        [HttpGet("{id:length(24)}", Name = "GetSong")]
        public async Task<ActionResult<Song>> GetById(string id)
        {
            var Song = await _songService.GetByIdAsync(id);
            if (Song == null)
            {
                return NotFound();
            }
            return Ok(Song);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Song Song)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _songService.CreateAsync(Song);
            return Ok(Song);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Song updatedSong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedSong = await _songService.GetByIdAsync(id);
            if (queriedSong == null)
            {
                return NotFound();
            }
            await _songService.UpdateAsync(id, updatedSong);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Song = await _songService.GetByIdAsync(id);
            if (Song == null)
            {
                return NotFound();
            }
            await _songService.DeleteAsync(id);
            return NoContent();
        }
    }
}
