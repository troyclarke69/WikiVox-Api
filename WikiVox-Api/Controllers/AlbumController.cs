using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly AlbumService _albumService;

        public AlbumController(AlbumService service)
        {
            _albumService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Album>>> GetAll()
        {
            var Albums = await _albumService.GetAllAsync();
            return Ok(Albums);
        }

        [HttpGet("{id:length(24)}", Name = "GetAlbum")]
        public async Task<ActionResult<Album>> GetById(string id)
        {
            var Album = await _albumService.GetByIdAsync(id);
            if (Album == null)
            {
                return NotFound();
            }
            return Ok(Album);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Album Album)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _albumService.CreateAsync(Album);
            return Ok(Album);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Album updatedAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedAlbum = await _albumService.GetByIdAsync(id);
            if (queriedAlbum == null)
            {
                return NotFound();
            }
            await _albumService.UpdateAsync(id, updatedAlbum);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Album = await _albumService.GetByIdAsync(id);
            if (Album == null)
            {
                return NotFound();
            }
            await _albumService.DeleteAsync(id);
            return NoContent();
        }
    }
}
