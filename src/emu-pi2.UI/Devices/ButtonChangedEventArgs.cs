using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emu_pi2.UI.Devices
{
    public class ButtonChangedEventArgs : EventArgs
    {
        public List<ButtonStatus> Buttons { get; set; }
    }
}
