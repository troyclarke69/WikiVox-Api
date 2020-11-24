using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistGenreController : ControllerBase
    {
        private readonly ArtistGenreService _artistGenreService;
        private readonly GenreService _genreService;
        private readonly ArtistService _artistService;

        public ArtistGenreController(ArtistGenreService service, GenreService GenreService,
                    ArtistService ArtistService)
        {
            _artistGenreService = service;
            _genreService = GenreService;
            _artistService = ArtistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistGenre>>> GetAll()
        {
            var ArtistGenre = await _artistGenreService.GetAllAsync();
            return Ok(ArtistGenre);
        }

        [HttpGet("{id:length(24)}", Name = "GetArtistGenre")]
        public async Task<ActionResult<ArtistGenre>> GetById(string id)
        {
            var ArtistGenre = await _artistGenreService.GetByIdAsync(id);
            if (ArtistGenre == null)
            {
                return NotFound();
            }

            // artist data
            if (ArtistGenre.Artist.Length > 0)
            {
                var artist = await _artistService.GetByIdAsync(ArtistGenre.Artist);
                if (artist != null)
                {
                    ArtistGenre.ArtistData = artist;
                }
            }
            // genre data
            if (ArtistGenre.Genres.Count > 0)
            {
                var genreList = new List<Genre>();
                foreach (var _genre in ArtistGenre.Genres)
                {
                    var genre = await _genreService.GetByIdAsync(_genre);
                    if (genre != null)
                        genreList.Add(genre);
                }
                ArtistGenre.GenreData = genreList;
            }

            return Ok(ArtistGenre);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArtistGenre ArtistGenre)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _artistGenreService.CreateAsync(ArtistGenre);
            return Ok(ArtistGenre);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ArtistGenre updatedArtistGenre)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedArtistGenre = await _artistGenreService.GetByIdAsync(id);
            if (queriedArtistGenre == null)
            {
                return NotFound();
            }
            await _artistGenreService.UpdateAsync(id, updatedArtistGenre);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var AlbumGenre = await _artistGenreService.GetByIdAsync(id);
            if (AlbumGenre == null)
            {
                return NotFound();
            }
            await _artistGenreService.DeleteAsync(id);
            return NoContent();
        }
    }
}
