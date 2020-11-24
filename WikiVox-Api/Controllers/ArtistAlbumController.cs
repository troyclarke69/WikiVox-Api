using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistAlbumController : ControllerBase
    {
        private readonly ArtistAlbumService _artistAlbumService;
        private readonly AlbumService _albumService;
        private readonly ArtistService _artistService;

        public ArtistAlbumController(ArtistAlbumService service, AlbumService AlbumService, 
                    ArtistService ArtistService)
        {
            _artistAlbumService = service;
            _albumService = AlbumService;
            _artistService = ArtistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistAlbum>>> GetAll()
        {
            var ArtistAlbum = await _artistAlbumService.GetAllAsync();
            return Ok(ArtistAlbum);
        }

        [HttpGet("{id:length(24)}", Name = "GetArtistAlbum")]
        public async Task<ActionResult<ArtistAlbum>> GetById(string id)
        {
            var ArtistAlbum = await _artistAlbumService.GetByIdAsync(id);
            if (ArtistAlbum == null)
            {
                return NotFound();
            }

            // artist data
            if (ArtistAlbum.Artist.Length > 0)
            {
                var artist = await _artistService.GetByIdAsync(ArtistAlbum.Artist);
                if (artist != null)
                {
                    ArtistAlbum.ArtistData = artist;
                }
            }
            // album data
            if (ArtistAlbum.Albums.Count > 0)
            {
                var albumList = new List<Album>();
                foreach (var _album in ArtistAlbum.Albums)
                {
                    var album = await _albumService.GetByIdAsync(_album);
                    if (album != null)
                        albumList.Add(album);
                }
                ArtistAlbum.AlbumData = albumList;
            }

            return Ok(ArtistAlbum);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArtistAlbum ArtistAlbum)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _artistAlbumService.CreateAsync(ArtistAlbum);
            return Ok(ArtistAlbum);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ArtistAlbum updatedArtistAlbum)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedArtistAlbum = await _artistAlbumService.GetByIdAsync(id);
            if (queriedArtistAlbum == null)
            {
                return NotFound();
            }
            await _artistAlbumService.UpdateAsync(id, updatedArtistAlbum);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var AlbumSong = await _artistAlbumService.GetByIdAsync(id);
            if (AlbumSong == null)
            {
                return NotFound();
            }
            await _artistAlbumService.DeleteAsync(id);
            return NoContent();
        }
    }
}
