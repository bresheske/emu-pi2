using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace emu_pi2.UI.Devices
{
    public class ButtonStatus
    {
        public enum ButtonType
        {
            None,
            Menu,
            View,
            A,
            B,
            X,
            Y,
            Up,
            Down,
            Left,
            Right,
            LeftThumbstick,
            RightThumbstick,
            LeftShoulder,
            RightShoulder
        }

        public enum ButtonStatusType
        {
            Pressed,
            Unpressed
        }

        public ButtonType Button { get; set; }
        public ButtonStatusType Status { get; set; }
    }
}
