using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emu_pi2.UI.Devices
{
    public class GamePadButtonStatus
    {
        public bool None { get; set; }
        public bool Menu { get; set; }
        public bool View { get; set; }
        public bool A { get; set; }
        public bool B { get; set; }
        public bool X { get; set; }
        public bool Y { get; set; }
        public bool Up { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool LeftThumbstick { get; set; }
        public bool RightThumbstick { get; set; }
        public bool LeftShoulder { get; set; }
        public bool RightShoulder { get; set; }
    }
}
