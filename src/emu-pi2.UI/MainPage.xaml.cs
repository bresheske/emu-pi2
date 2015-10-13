using emu_pi2.Core.Logging;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace emu_pi2.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly IConsoleRepository _consolerepository;
        private readonly MainViewModel _viewmodel;

        public MainPage()
        {
            this.InitializeComponent();
            _consolerepository = ServiceFactory.Current.Create<IConsoleRepository>();
            _viewmodel = new MainViewModel();

            _viewmodel.Consoles = _consolerepository.GetAll();


            this.DataContext = _viewmodel;
        }
    }
}
