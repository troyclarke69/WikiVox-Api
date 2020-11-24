using System.Collections.Generic;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface IArtist
    {
        IEnumerable<Artist> GetAll();
        Artist Get(int id);
        //IEnumerable<Artist> GetArtistsByGenre(int id);
        void Add(Artist newArtist);
        void Update(Artist newArtist);
        void Delete(int id);
    }
}
