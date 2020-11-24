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
    public class MusicianController : ControllerBase
    {
        private readonly MusicianService _musicianService;

        public MusicianController(MusicianService service)
        {
            _musicianService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Musician>>> GetAll()
        {
            var Musicians = await _musicianService.GetAllAsync();
            return Ok(Musicians);
        }

        [HttpGet("{id:length(24)}", Name = "GetMusician")]
        public async Task<ActionResult<Musician>> GetById(string id)
        {
            var Musician = await _musicianService.GetByIdAsync(id);
            if (Musician == null)
            {
                return NotFound();
            }
            return Ok(Musician);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Musician Musician)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _musicianService.CreateAsync(Musician);
            return Ok(Musician);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Musician updatedMusician)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedMusician = await _musicianService.GetByIdAsync(id);
            if (queriedMusician == null)
            {
                return NotFound();
            }
            await _musicianService.UpdateAsync(id, updatedMusician);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Musician = await _musicianService.GetByIdAsync(id);
            if (Musician == null)
            {
                return NotFound();
            }
            await _musicianService.DeleteAsync(id);
            return NoContent();
        }
    }
}
