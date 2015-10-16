using emu_pi2.Core.Logging;
using emu_pi2.Data.Objects;
using emu_pi2.Data.Services;
using emu_pi2.Data.Services.Factory;
using emu_pi2.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using emu_pi2.UI.Extensions;
using emu_pi2.UI.Devices;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace emu_pi2.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const string VERSION = "1.0.0";
        public const int MAX_CONSOLE_COLUMNS = 5;

        private readonly IConsoleRepository _consolerepository;
        private readonly MainViewModel _viewmodel;

        private bool _isstateone = true;

        private enum ActionType
        {
            None,
            Up,
            Left,
            Down,
            Right,
            Select
        };

        public MainPage()
        {
            this.InitializeComponent();
            _consolerepository = ServiceFactory.Current.Create<IConsoleRepository>();
            _viewmodel = MainViewModel.Current;

            _viewmodel.Consoles = _consolerepository.GetAll();
            _viewmodel.Version = "Version: " + VERSION;

            this.DataContext = _viewmodel;
            Window.Current.CoreWindow.KeyUp += PageKeyStroke;
            GamePad.Current.ButtonChanged += GamePadChanged;
            GamePad.Current.Start(MainViewModel.Current.Dispatcher);
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // Focus the first Console.
            FocusConsole(_viewmodel.Consoles.First());
            LoadIn.Begin();
        }

        private void ShowBackgroundImage(string src)
        {
            var animation = _isstateone
                ? ShowImage2
                : ShowImage1;
            
            src = "ms-appx:///" + src;
            var source = new BitmapImage(new Uri(src));
            if (_isstateone)
            {
                BackgroundImage2.Source = source;
            }
            else
            {
                BackgroundImage1.Source = source;
            }
            ShowImage1.Stop();
            ShowImage2.Stop();
            animation.Begin();
            _isstateone = !_isstateone;
        }

        private void FocusConsole(Console console)
        {
            var previousconsole = _viewmodel.SelectedConsole;
            var prevgrid = FindConsoleGrid(previousconsole);
            var newgrid = FindConsoleGrid(console);

            // No action if we're on the first console and we hit 'left' or something.
            if (console == previousconsole)
                return;
        
            _viewmodel.SelectedConsole = console;

            ConsoleGainFocus(newgrid);

            // prevgrid could be null if it's the first selection.
            if (prevgrid != null)
                ConsoleLoseFocus(prevgrid);
        }

        private void ConsoleGainFocus(FrameworkElement grid)
        {
            ShowBackgroundImage(((Console)grid.DataContext).BackgroundLink);
            ConsoleFocus.Stop();
            Storyboard.SetTarget(ConsoleFocus, grid);
            ConsoleFocus.Begin();
        }

        private void ConsoleLoseFocus(FrameworkElement grid)
        {
            ConsoleUnFocus.Stop();
            Storyboard.SetTarget(ConsoleUnFocus, grid);
            ConsoleUnFocus.Begin();
        }

        private void GamePadChanged(object sender, ButtonChangedEventArgs args)
        {
            if (args.Buttons.Any(x => x.Button == ButtonStatus.ButtonType.A))
                PerformAction(ActionType.Select);
            else if (args.Buttons.Any(x => x.Button == ButtonStatus.ButtonType.Left))
                PerformAction(ActionType.Left);
            else if (args.Buttons.Any(x => x.Button == ButtonStatus.ButtonType.Right))
                PerformAction(ActionType.Right);
            else if (args.Buttons.Any(x => x.Button == ButtonStatus.ButtonType.Up))
                PerformAction(ActionType.Up);
            else if (args.Buttons.Any(x => x.Button == ButtonStatus.ButtonType.Down))
                PerformAction(ActionType.Down);
        }

        private void PageKeyStroke(object sender, KeyEventArgs e)
        {
            // In here we need to manage the focused console.
            var action = GetAction(e.VirtualKey);
            if (action != ActionType.None)
                PerformAction(action);
        }

        private ActionType GetAction(VirtualKey key)
        {
            if (key == VirtualKey.Up || key == VirtualKey.GamepadDPadUp)
                return ActionType.Up;
            else if (key == VirtualKey.Down || key == VirtualKey.GamepadDPadDown)
                return ActionType.Down;
            else if (key == VirtualKey.Left || key == VirtualKey.GamepadDPadLeft)
                return ActionType.Left;
            else if (key == VirtualKey.Right || key == VirtualKey.GamepadDPadRight)
                return ActionType.Right;
            else if (key == VirtualKey.Select || key == VirtualKey.Enter || key == VirtualKey.GamepadMenu)
                return ActionType.Select;
            else
                return ActionType.None;
        }

        private FrameworkElement FindConsoleGrid(Console console)
        {
            if (console == null)
                return null;
            ConsoleListGrid.UpdateLayout();
            var fe = (FrameworkElement)ConsoleListGrid.ContainerFromItem(console);
            var grid = ((DependencyObject)fe).FindChildByType<Grid>();
            return grid;
        }

        private void PerformAction(ActionType action)
        {
            var selectedconsole = _viewmodel.SelectedConsole;
            
            if (action == ActionType.Select)
            {
                // Clean up window handlers.
                Window.Current.CoreWindow.KeyUp -= PageKeyStroke;

                // Handle selection and not movement.
                _viewmodel.SoundSelect.Play();
                _viewmodel.LayoutRoot.NavigateToWithTransition(typeof(GamesListPage), LoadOut);
            }
            else
            {
                // Handle movement.

                // Finds the index of the selected console.
                var index = _viewmodel.Consoles
                    .Select((x, i) => new { Obj = x, Index = i })
                    .First(x => x.Obj == selectedconsole).Index;

                // Sets the desired change in index.
                var indexdelta = 0;
                if (action == ActionType.Left)
                    indexdelta = -1;
                else if (action == ActionType.Right)
                    indexdelta = 1;
                else if (action == ActionType.Up)
                    indexdelta = -MAX_CONSOLE_COLUMNS;
                else if (action == ActionType.Down)
                    indexdelta = MAX_CONSOLE_COLUMNS;

                // Makes sure we stay within out array bounds.
                index += indexdelta;
                if (index < 0)
                    index = 0;
                if (index >= _viewmodel.Consoles.Count())
                    index = _viewmodel.Consoles.Count() - 1;

                // Performs the focus changes.
                var newconsole = _viewmodel.Consoles.ElementAt(index);
                FocusConsole(newconsole);
            }
           
        }

    }
}
