using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly ArtistService _artistService;
        private readonly ImageService _imageService;
        private readonly MusicianService _musicianService;
        private readonly AlbumService _albumService;

        public ArtistController(ArtistService service, ImageService ImageService, 
                                MusicianService MusicianService, AlbumService AlbumService)
        {
            _artistService = service;
            _imageService = ImageService;
            _musicianService = MusicianService;
            _albumService = AlbumService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetAll()
        {
            var artists = await _artistService.GetAllAsync();
            List<Artist> list = new List<Artist>();

            foreach (var _artist in artists)
            {
                // record may have no 'Images' tag, or may have 0 or more IDs
                if (_artist.Images != null)
                {
                    if (_artist.Images.Count > 0)
                    {
                        var imageList = new List<Image>();
                        foreach (var _image in _artist.Images)
                        {
                            var image = await _imageService.GetByIdAsync(_image);
                            if (image != null)
                                imageList.Add(image);
                        }
                        _artist.ImageData = imageList;
                    }
                }

                // musican data
                //if (_artist.Musicians != null)
                //{
                //    if (_artist.Musicians.Count > 0)
                //    {
                //        var musicianList = new List<Musician>();
                //        foreach (var _musician in _artist.Musicians)
                //        {
                //            var musician = await _musicianService.GetByIdAsync(_musician);
                //            if (musician != null)
                //                musicianList.Add(musician);
                //        }
                //        _artist.MusicianData = musicianList;
                //    }
                //}

                // album data
                //if (_artist.Albums != null)
                //{
                //    if (_artist.Albums.Count > 0)
                //    {
                //        var albumList = new List<Album>();
                //        foreach (var _album in _artist.Albums)
                //        {
                //            var album = await _albumService.GetByIdAsync(_album);
                //            if (album != null)
                //                albumList.Add(album);
                //        }
                //        _artist.AlbumData = albumList;
                //    }
                //}

                list.Add(_artist);
            }
            
            return Ok(list);
        }

        [Route("[action]/{artistName}")]
        [HttpGet]
        //[HttpGet("{id:length(24)}", Name = "SearchByArtistName")]
        public async Task<ActionResult<IEnumerable<Artist>>> SearchByArtistName(string artistName)
        {
            var artists = await _artistService.GetAllByArtistNameAsync(artistName);
            List<Artist> list = new List<Artist>();

            foreach (var _artist in artists)
            {
                // record may have no 'Images' tag, or may have 0 or more IDs
                if (_artist.Images != null)
                {
                    if (_artist.Images.Count > 0)
                    {
                        var imageList = new List<Image>();
                        foreach (var _image in _artist.Images)
                        {
                            var image = await _imageService.GetByIdAsync(_image);
                            if (image != null)
                                imageList.Add(image);
                        }
                        _artist.ImageData = imageList;
                    }
                }

                // musican data
                //if (_artist.Musicians != null)
                //{
                //    if (_artist.Musicians.Count > 0)
                //    {
                //        var musicianList = new List<Musician>();
                //        foreach (var _musician in _artist.Musicians)
                //        {
                //            var musician = await _musicianService.GetByIdAsync(_musician);
                //            if (musician != null)
                //                musicianList.Add(musician);
                //        }
                //        _artist.MusicianData = musicianList;
                //    }
                //}

                // album data
                //if (_artist.Albums != null)
                //{
                //    if (_artist.Albums.Count > 0)
                //    {
                //        var albumList = new List<Album>();
                //        foreach (var _album in _artist.Albums)
                //        {
                //            var album = await _albumService.GetByIdAsync(_album);
                //            if (album != null)
                //                albumList.Add(album);
                //        }
                //        _artist.AlbumData = albumList;
                //    }
                //}

                list.Add(_artist);
            }

            return Ok(list);
        }


        [HttpGet("{id:length(24)}", Name = "GetArtist")]
        public async Task<ActionResult<Artist>> GetById(string id)
        {
            var Artist = await _artistService.GetByIdAsync(id);
            if (Artist == null)
            {
                return NotFound();
            }

            // image data
            if (Artist.Images != null)
            {
                if (Artist.Images.Count > 0)
                {
                    var imageList = new List<Image>();
                    foreach (var _image in Artist.Images)
                    {
                        var image = await _imageService.GetByIdAsync(_image);
                        if (image != null)
                            imageList.Add(image);
                    }
                    Artist.ImageData = imageList;
                }
            }

            // musican data
            if (Artist.Musicians != null)
            {
                if (Artist.Musicians.Count > 0)
                {
                    var musicianList = new List<Musician>();
                    foreach (var _musician in Artist.Musicians)
                    {
                        var musician = await _musicianService.GetByIdAsync(_musician);
                        if (musician != null)
                            musicianList.Add(musician);
                    }
                    Artist.MusicianData = musicianList;
                }
            }

            // album data
            if (Artist.Albums != null)
            {
                if (Artist.Albums.Count > 0)
                {
                    var albumList = new List<Album>();
                    foreach (var _album in Artist.Albums)
                    {
                        var album = await _albumService.GetByIdAsync(_album);
                        if (album != null)
                            albumList.Add(album);
                    }
                    Artist.AlbumData = albumList;
                }
            }

            return Ok(Artist);
        }

        //[HttpGet("{id:length(24)}", Name = "GetArtist")]
        //public async Task<ActionResult<Artist>> GetById(string id)
        //{
        //    var Artist = await _artistService.GetByIdAsync(id);
        //    if (Artist == null)
        //    {
        //        return NotFound();
        //    }

        //    // image data
        //    if (Artist.Images.Count > 0)
        //    {
        //        var imageList = new List<Image>();
        //        foreach (var _image in Artist.Images)
        //        {
        //            var image = await _imageService.GetByIdAsync(_image);
        //            if (image != null)
        //                imageList.Add(image);
        //        }
        //        Artist.ImageData = imageList;
        //    }

        //    // musican data
        //    if (Artist.Musicians.Count > 0)
        //    {
        //        var musicianList = new List<Musician>();
        //        foreach (var _musician in Artist.Musicians)
        //        {
        //            var musician = await _musicianService.GetByIdAsync(_musician);
        //            if (musician != null)
        //                musicianList.Add(musician);
        //        }
        //        Artist.MusicianData = musicianList;
        //    }

        //    // album data
        //    if (Artist.Albums != null)
        //    {
        //        if (Artist.Albums.Count > 0)
        //        {
        //            var albumList = new List<Album>();
        //            foreach (var _album in Artist.Albums)
        //            {
        //                var album = await _albumService.GetByIdAsync(_album);
        //                if (album != null)
        //                    albumList.Add(album);
        //            }
        //            Artist.AlbumData = albumList;
        //        }
        //    }

        //    return Ok(Artist);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(Artist Artist)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _artistService.CreateAsync(Artist);
            return Ok(Artist);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Artist updatedArtist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedArtist = await _artistService.GetByIdAsync(id);
            if (queriedArtist == null)
            {
                return NotFound();
            }
            await _artistService.UpdateAsync(id, updatedArtist);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Artist = await _artistService.GetByIdAsync(id);
            if (Artist == null)
            {
                return NotFound();
            }
            await _artistService.DeleteAsync(id);
            return NoContent();
        }
    }
}
