using System;
using System.Collections.Generic;
using System.Text;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface IMusician
    {
        IEnumerable<Musician> GetAll();
        Musician Get(int id);
        IEnumerable<Musician> GetAllByArtist(int id);
        IEnumerable<Musician> GetAllByInstrument(int id);
        void Add(Musician newMusician);
        void Update(Musician newMusician);
        void Delete(int id);
    }
}
