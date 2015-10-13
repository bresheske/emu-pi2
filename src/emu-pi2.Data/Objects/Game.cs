using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emu_pi2.Data.Objects
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageBase64 { get; set; }
        public string Description { get; set; }
        public string Developer { get; set; }
    }
}
