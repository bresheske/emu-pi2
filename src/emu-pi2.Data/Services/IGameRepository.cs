using emu_pi2.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emu_pi2.Data.Services
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAll();
        Game Get(int id);
        IEnumerable<Game> GetAllForConsole(int consoleid);
    }
}
