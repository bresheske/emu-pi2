using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using emu_pi2.Data.Objects;

namespace emu_pi2.Data.Services
{
    public class MockConsoleRepository : IConsoleRepository
    {
        public IEnumerable<Console> GetAll()
        {
            return new List<Console>()
            {
                new Console() {Id = 1, ShortName = "NES", LongName = "Nintendo Entertainment System" },
                new Console() {Id = 2, ShortName = "SNES", LongName = "Super Nintendo Entertainment System" },
                new Console() {Id = 3, ShortName = "PS1", LongName = "PlayStation" },
                new Console() {Id = 3, ShortName = "N64", LongName = "Nintendo 64" },
            };
        }
    }
}
