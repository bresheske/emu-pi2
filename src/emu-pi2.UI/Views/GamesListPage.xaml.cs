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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace emu_pi2.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamesListPage : Page
    {
        private readonly IGameRepository _gamerepository;

        public GamesListPage()
        {
            this.InitializeComponent();
            _gamerepository = ServiceFactory.Current.Create<IGameRepository>();

            MainViewModel.Current.Games = _gamerepository.GetAllForConsole(MainViewModel.Current.SelectedConsole.Id);
            this.DataContext = MainViewModel.Current;
        }
    }
}
