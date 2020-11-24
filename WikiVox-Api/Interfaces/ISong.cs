using System;
using System.Collections.Generic;
using System.Text;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface ISong
    {
        IEnumerable<Song> GetAll();
        Song Get(int id);
        void Add(Song newSong);
        void Update(Song newSong);
        void Delete(int id);
    }
}
