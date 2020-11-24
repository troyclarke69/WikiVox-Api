using System;
using System.Collections.Generic;
using System.Text;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface IAlbumSong
    {
        IEnumerable<AlbumSong> GetRandomSongsByArtist(int id);
        AlbumSong GetAlbumByAlbum(int id);

        IEnumerable<AlbumSong> GetAllAlbumsByArtist(int id);
        AlbumSong GetAlbumByArtist(int id);

        IEnumerable<AlbumSong> GetAllSongsByArtist(int id);
        AlbumSong GetSongByArtist(int id);

        IEnumerable<AlbumSong> GetAllSongsByAlbum(int id);
        AlbumSong GetSongByAlbum(int id);

        void Add(int albumId, int artistId, int trackNumber, int songId);

        IEnumerable<AlbumSong> GetAll();
        AlbumSong Get(int id);
        //void Add(AlbumSong newAlbumSong);
        void Update(AlbumSong newAlbumSong);
        void Delete(int id);
    }
}
