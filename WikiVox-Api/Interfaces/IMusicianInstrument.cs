using System;
using System.Collections.Generic;
using System.Text;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface IMusicianInstrument
    {
        IEnumerable<MusicianInstrument> GetAll();
        MusicianInstrument Get(int id);
        bool IsMarked(int musicianId, int instrumentId);
        void Add(int musicianId, int instrumentId);
        void Delete(int musicianId, int instrumentId);
        //void Add(MusicianInstrument newMusicianInstrument);
        //void Update(MusicianInstrument newMusicianInstrument);
        //void Delete(int id);
    }
}
