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
    public class ImageController : ControllerBase
    {
        private readonly ImageService _imageService;
        private readonly EntityService _entityService;
        private readonly GenreService _genreService;
        private readonly ArtistService _artistService;
        private readonly AlbumService _albumService;
        private readonly SongService _songService;
        //future use:
        //private readonly MusicianService _musicianService;
        //private readonly InstrumentService _instrumentService;

        public ImageController(ImageService service, EntityService EntityService, GenreService GenreService,
                    ArtistService ArtistService, AlbumService AlbumService, SongService SongService)
        {
            _imageService = service;
            _entityService = EntityService;
            _genreService = GenreService;
            _artistService = ArtistService;
            _albumService = AlbumService;
            _songService = SongService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetAll()
        {
            var Image = await _imageService.GetAllAsync();
            return Ok(Image);
        }

        [HttpGet("{id:length(24)}", Name = "GetImage")]
        public async Task<ActionResult<Image>> GetById(string id)
        {
            var Image = await _imageService.GetByIdAsync(id);
            if (Image == null)
            {
                return NotFound();
            }

            // entity data
            if (Image.Entity.Length > 0)
            {
                var entity = await _entityService.GetByIdAsync(Image.Entity);
                if (entity != null)
                {
                    Image.EntityData = entity;
                }
            }

            // resource (object) data
            if (Image.Resource.Length > 0)
            {
                var _id = Image.Resource;

                if (Image.EntityData.Name == "Album")
                {
                    var obj = await _albumService.GetByIdAsync(_id);
                    if (obj != null)
                    {
                        Image.ResourceData = obj;
                    }
                }
                if (Image.EntityData.Name == "Artist")
                {
                    var obj = await _artistService.GetByIdAsync(_id);
                    if (obj != null)
                    {
                        Image.ResourceData = obj;
                    }
                }
                if (Image.EntityData.Name == "Genre")
                {
                    var obj = await _genreService.GetByIdAsync(_id);
                    if (obj != null)
                    {
                        Image.ResourceData = obj;
                    }
                }
                if (Image.EntityData.Name == "Song")
                {
                    var obj = await _songService.GetByIdAsync(_id);
                    if (obj != null)
                    {
                        Image.ResourceData = obj;
                    }
                }
                // To do: Instrument, Musician

            }

            return Ok(Image);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Image Image)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _imageService.CreateAsync(Image);
            return Ok(Image);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Image updatedImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedImage = await _imageService.GetByIdAsync(id);
            if (queriedImage == null)
            {
                return NotFound();
            }
            await _imageService.UpdateAsync(id, updatedImage);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Image = await _imageService.GetByIdAsync(id);
            if (Image == null)
            {
                return NotFound();
            }
            await _imageService.DeleteAsync(id);
            return NoContent();
        }
    }
}
