using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
    public interface IWikivox_ApiDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string AlbumCollectionName { get; set; }
        string AlbumSongCollectionName { get; set; }
        string ArtistCollectionName { get; set; }
        string ArtistAlbumCollectionName { get; set; }
        string ArtistGenreCollectionName { get; set; }
        string ArtistImageCollectionName { get; set; }

        string ArtistMusicianCollectionName { get; set; }
        string ArtistSongCollectionName { get; set; }
        string EntityCollectionName { get; set; }
        string GenreCollectionName { get; set; }
        string ImageCollectionName { get; set; }
        string InstrumentCollectionName { get; set; }
        string MusicianCollectionName { get; set; }
        string MusicianInstrumentCollectionName { get; set; }
        string SongCollectionName { get; set; }
    }

    public class Wikivox_ApiDatabaseSettings : IWikivox_ApiDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string AlbumCollectionName { get; set; }
        public string AlbumSongCollectionName { get; set; }
        public string ArtistCollectionName { get; set; }
        public string ArtistAlbumCollectionName { get; set; }
        public string ArtistGenreCollectionName { get; set; }
        public string ArtistImageCollectionName { get; set; }

        public string ArtistMusicianCollectionName { get; set; }
        public string ArtistSongCollectionName { get; set; }
        public string EntityCollectionName { get; set; }
        public string GenreCollectionName { get; set; }
        public string ImageCollectionName { get; set; }
        public string InstrumentCollectionName { get; set; }
        public string MusicianCollectionName { get; set; }
        public string MusicianInstrumentCollectionName { get; set; }
        public string SongCollectionName { get; set; }
    }
}
