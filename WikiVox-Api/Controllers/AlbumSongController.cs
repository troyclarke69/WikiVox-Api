using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wikivox_Api.Models;
using Wikivox_Api.Services;

namespace Wikivox_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumSongController : ControllerBase
    {
        private readonly AlbumSongService _albumSongService;
        private readonly SongService _songService;
        private readonly AlbumService _albumService;

        public AlbumSongController(AlbumSongService service, SongService SongService, 
                AlbumService AlbumService)
        {
            _albumSongService = service;
            _songService = SongService;
            _albumService = AlbumService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumSong>>> GetAll()
        {
            var AlbumSongs = await _albumSongService.GetAllAsync();
            return Ok(AlbumSongs);
        }

        [HttpGet("{id:length(24)}", Name = "GetAlbumSong")]
        public async Task<ActionResult<AlbumSong>> GetById(string id)
        {
            var AlbumSong = await _albumSongService.GetByIdAsync(id);
            if (AlbumSong == null)
            {
                return NotFound();
            }
            // album data
            if (AlbumSong.Album.Length > 0)
            {
                var album = await _albumService.GetByIdAsync(AlbumSong.Album);
                if (album != null)
                {
                    AlbumSong.AlbumData = album;
                }
            }
            // song data
            if (AlbumSong.Songs.Count > 0)
            {
                var songList = new List<Song>();
                foreach(var _song in AlbumSong.Songs)
                {
                    var song = await _songService.GetByIdAsync(_song);
                    if (song != null)
                        songList.Add(song);
                }
                AlbumSong.SongData = songList;
            }

            return Ok(AlbumSong);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AlbumSong AlbumSong)
        {
            // got all required fields?
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _albumSongService.CreateAsync(AlbumSong);
            return Ok(AlbumSong);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, AlbumSong updatedAlbumSong)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var queriedAlbumSong = await _albumSongService.GetByIdAsync(id);
            if (queriedAlbumSong == null)
            {
                return NotFound();
            }
            await _albumSongService.UpdateAsync(id, updatedAlbumSong);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var AlbumSong = await _albumSongService.GetByIdAsync(id);
            if (AlbumSong == null)
            {
                return NotFound();
            }
            await _albumSongService.DeleteAsync(id);
            return NoContent();
        }
    }
}
