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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace emu_pi2.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public const string VERSION = "1.0.0";

        private readonly IConsoleRepository _consolerepository;
        private readonly MainViewModel _viewmodel;

        private bool _isstateone = true;

        public MainPage()
        {
            this.InitializeComponent();
            _consolerepository = ServiceFactory.Current.Create<IConsoleRepository>();
            _viewmodel = new MainViewModel();

            _viewmodel.Consoles = _consolerepository.GetAll();
            _viewmodel.Version = "Version: " + VERSION;

            this.DataContext = _viewmodel;
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

            animation.Begin();
            _isstateone = !_isstateone;
        }

        private void ConsoleGainedFocus(object sender, PointerRoutedEventArgs e)
        {
            _viewmodel.SelectedConsole = (Console)((Grid)sender).DataContext;
            ShowBackgroundImage(_viewmodel.SelectedConsole.BackgroundLink);
        }
    }
}
