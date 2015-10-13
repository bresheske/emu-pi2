using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emu_pi2.Data.Objects
{
    public class Console
    {
        [Key]
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string BackgroundLink { get; set; }

    }
}
