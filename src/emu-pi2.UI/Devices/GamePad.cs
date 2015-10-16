using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Windows.Gaming.Input;
using Windows.UI.Core;

namespace emu_pi2.UI.Devices
{
    public class GamePad : IDisposable
    {
        private static GamePad _instance;
        private Gamepad _msftpad;
        private bool _islistening;
        private Timer _timer;
        private CoreDispatcher _dispatcher;
        public event EventHandler<ButtonChangedEventArgs> ButtonChanged;

        private GamePadButtonStatus _previousbuttons;

        private GamePad()
        {
            _islistening = false;
            var foundpad = false;

            // This is fucking strange, but it takes 2-3 calls to the Gamepads[] array
            // before anything is returned.
            while (!foundpad)
            {
                try
                {
                    _msftpad = Gamepad.Gamepads[0];
                    foundpad = true;
                }
                catch { }
            }
            
        }

        public static GamePad Current
        {
            get
            {
                if (_instance == null)
                    _instance = new GamePad();
                return _instance;
            }
        }

        public void Start(CoreDispatcher callbackdispatcher)
        {
            if (_islistening)
                return;

            _dispatcher = callbackdispatcher;
            _islistening = true;
            var timercallback = new TimerCallback(PerformGamePadCheck);
            _timer = new Timer(timercallback, null, 100, 100);
        }

        private void PerformGamePadCheck(object state)
        {
            // See here for explainations.
            // https://msdn.microsoft.com/en-us/library/windows/apps/windows.gaming.input.gamepadbuttons
            var buttonsd = _msftpad.GetCurrentReading().Buttons;
            var currentbuttons = ConvertButtonsFromMsft(buttonsd);

            if (_previousbuttons == null)
                _previousbuttons = currentbuttons;

            var changedbuttons = new List<ButtonStatus>();
            // now we just need to compare the statuses.
            if (currentbuttons.None && !_previousbuttons.None)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.None, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if(!currentbuttons.None && _previousbuttons.None)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.None, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.Menu && !_previousbuttons.Menu)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Menu, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.Menu && _previousbuttons.Menu)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Menu, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.View && !_previousbuttons.View)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.View, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.View && _previousbuttons.View)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.View, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.A && !_previousbuttons.A)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.A, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.A && _previousbuttons.A)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.A, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.B && !_previousbuttons.B)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.B, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.B && _previousbuttons.B)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.B, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.X && !_previousbuttons.X)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.X, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.X && _previousbuttons.X)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.X, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.Y && !_previousbuttons.Y)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Y, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.Y && _previousbuttons.Y)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Y, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.Up && !_previousbuttons.Up)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Up, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.Up && _previousbuttons.Up)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Up, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.Down && !_previousbuttons.Down)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Down, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.Down && _previousbuttons.Down)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Down, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.Left && !_previousbuttons.Left)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Left, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.Left && _previousbuttons.Left)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Left, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.Right && !_previousbuttons.Right)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Right, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.Right && _previousbuttons.Right)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.Right, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.LeftShoulder && !_previousbuttons.LeftShoulder)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.LeftShoulder, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.LeftShoulder && _previousbuttons.LeftShoulder)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.LeftShoulder, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.RightShoulder && !_previousbuttons.RightShoulder)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.RightShoulder, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.RightShoulder && _previousbuttons.RightShoulder)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.RightShoulder, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.LeftThumbstick && !_previousbuttons.LeftThumbstick)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.LeftThumbstick, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.LeftThumbstick && _previousbuttons.LeftThumbstick)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.LeftThumbstick, Status = ButtonStatus.ButtonStatusType.Unpressed });

            if (currentbuttons.RightThumbstick && !_previousbuttons.RightThumbstick)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.RightThumbstick, Status = ButtonStatus.ButtonStatusType.Pressed });
            else if (!currentbuttons.RightThumbstick && _previousbuttons.RightThumbstick)
                changedbuttons.Add(new ButtonStatus() { Button = ButtonStatus.ButtonType.RightThumbstick, Status = ButtonStatus.ButtonStatusType.Unpressed });

            // Finally fire the event if we need to.
            if (ButtonChanged != null && changedbuttons.Any())
            {
                _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    ButtonChanged(this, new ButtonChangedEventArgs() { Buttons = changedbuttons });
                });
            }
        }


        private GamePadButtonStatus ConvertButtonsFromMsft(GamepadButtons buttonsenum)
        {
            var gamepadbuttons = new GamePadButtonStatus();

            if (buttonsenum.HasFlag((GamepadButtons)0))
                gamepadbuttons.None = true;
            if (buttonsenum.HasFlag((GamepadButtons)1))
                gamepadbuttons.Menu = true;
            if (buttonsenum.HasFlag((GamepadButtons)2))
                gamepadbuttons.View = true;
            if (buttonsenum.HasFlag((GamepadButtons)4))
                gamepadbuttons.A = true;
            if (buttonsenum.HasFlag((GamepadButtons)8))
                gamepadbuttons.B = true;
            if (buttonsenum.HasFlag((GamepadButtons)16))
                gamepadbuttons.X = true;
            if (buttonsenum.HasFlag((GamepadButtons)32))
                gamepadbuttons.Y = true;
            if (buttonsenum.HasFlag((GamepadButtons)64))
                gamepadbuttons.Up = true;
            if (buttonsenum.HasFlag((GamepadButtons)128))
                gamepadbuttons.Down = true;
            if (buttonsenum.HasFlag((GamepadButtons)256))
                gamepadbuttons.Left = true;
            if (buttonsenum.HasFlag((GamepadButtons)512))
                gamepadbuttons.Right = true;
            if (buttonsenum.HasFlag((GamepadButtons)1024))
                gamepadbuttons.LeftShoulder = true;
            if (buttonsenum.HasFlag((GamepadButtons)2048))
                gamepadbuttons.RightShoulder = true;
            if (buttonsenum.HasFlag((GamepadButtons)4096))
                gamepadbuttons.LeftThumbstick = true;
            if (buttonsenum.HasFlag((GamepadButtons)8192))
                gamepadbuttons.RightThumbstick = true;

            return gamepadbuttons;
        }

        public void Stop()
        {
            _islistening = false;
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public void Dispose()
        {
            Stop();
        }

        
    }
}
