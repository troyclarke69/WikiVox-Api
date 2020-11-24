using System;
using System.Collections.Generic;
using System.Text;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface IArtistMusician
    {
        IEnumerable<ArtistMusician> GetAll();
        ArtistMusician Get(int id);
        IEnumerable<ArtistMusician> GetAllByArtist(int id);
        void Add(ArtistMusician newArtistMusician);
        void Update(ArtistMusician newArtistMusician);
        void Delete(int id);
    }
}
