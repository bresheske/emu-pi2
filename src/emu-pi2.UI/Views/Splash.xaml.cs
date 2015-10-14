using emu_pi2.Core.Logging;
using emu_pi2.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using emu_pi2.UI.Extensions;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace emu_pi2.UI.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Splash : Page
    {
        private bool _loaded;

        public Splash()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyUp += ClosePage;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // Start the Animation.
            LoadIn.Completed += (o, args) =>
            {
                _loaded = true;
            };
            LoadIn.Begin();
        }

        private void ClosePage(object sender, KeyEventArgs e)
        {
            if (_loaded)
            {
                _loaded = false;
                Window.Current.CoreWindow.KeyUp -= ClosePage;
                MainViewModel.Current.LayoutRoot.NavigateToWithTransition(typeof(MainPage), LoadOut);
            }
        }
    }
}
