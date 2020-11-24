using System;
using System.Collections.Generic;
using System.Text;
using Wikivox_Api.Models;

namespace Wikivox_Api.Interfaces
{
    public interface IInstrument
    {
        IEnumerable<Instrument> GetAll();
        Instrument Get(int id);
        void Add(Instrument newInstrument);
        void Update(Instrument newInstrument);
        void Delete(int id);
    }
}
