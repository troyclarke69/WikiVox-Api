using System.Collections.Generic;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface IArtistGenre
    {
        IEnumerable<ArtistGenre> GetAll();
        ArtistGenre Get(int id);
        ArtistGenre GetByArtistGenre(int genreId, int artistId);
        IEnumerable<ArtistGenre> GetArtistsByGenre(int id);
        IEnumerable<ArtistGenre> GetGenresByArtist(int id);
        bool isMarked(int genreId, int artistId);
        void Add(int genreId, int artistId);
        //void Update(ArtistGenre newArtistGenre);  // will not be used, only add/delete
        void Delete(int genreId, int artistId);
    }
}
