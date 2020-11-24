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
    public class ArtistImageController : ControllerBase
    {
        private readonly ArtistImageService _artistImageService;
        private readonly ImageService _imageService;
        private readonly ArtistService _artistService;

        public ArtistImageController(ArtistImageService service, ImageService ImageService,
                    ArtistService ArtistService)
        {
            _artistImageService = service;
            _imageService = ImageService;
            _artistService = ArtistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistImage>>> GetAll()
        {
            var ArtistImage = await _artistImageService.GetAllAsync();
            return Ok(ArtistImage);
        }

        [HttpGet("{id:length(24)}", Name = "GetArtistImage")]
        public async Task<ActionResult<ArtistImage>> GetById(string id)
        {
            var ArtistImage = await _artistImageService.GetByIdAsync(id);
            if (ArtistImage == null)
            {
                return NotFound();
            }

            // artist data
            //if (ArtistImage.Artist.Length > 0)
            //{
            //    var artist = await _artistService.GetByIdAsync(ArtistGenre.Artist);
            //    if (artist != null)
            //    {
            //        ArtistGenre.ArtistData = artist;
            //    }
            //}
            // genre data
            //if (ArtistGenre.Genres.Count > 0)
            //{
            //    var genreList = new List<Genre>();
            //    foreach (var _genre in ArtistGenre.Genres)
            //    {
            //        var genre = await _genreService.GetByIdAsync(_genre);
            //        if (genre != null)
            //            genreList.Add(genre);
            //    }
            //    ArtistGenre.GenreData = genreList;
            //}

            return Ok(ArtistImage);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ArtistImage ArtistImage)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _artistImageService.CreateAsync(ArtistImage);
            return Ok(ArtistImage);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, ArtistImage updatedArtistImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedArtistImage = await _artistImageService.GetByIdAsync(id);
            if (queriedArtistImage == null)
            {
                return NotFound();
            }
            await _artistImageService.UpdateAsync(id, updatedArtistImage);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var AlbumImage = await _artistImageService.GetByIdAsync(id);
            if (AlbumImage == null)
            {
                return NotFound();
            }
            await _artistImageService.DeleteAsync(id);
            return NoContent();
        }
    }
}
