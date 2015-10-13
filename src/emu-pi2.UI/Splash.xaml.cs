using emu_pi2.Core.Logging;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace emu_pi2.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Splash : Page
    {
        private bool _loaded;
        private MediaElement _music;

        public Splash()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.KeyUp += ClosePage;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            // Init the background music.
            _music = new MediaElement();
            _music.Source = new Uri("ms-appx:///Assets/bg-ambient2.mp3");
            _music.Volume = .5;
            _music.MediaEnded += (o, args) =>
            {
                var element = (MediaElement)o;
                element.Position = new TimeSpan(0, 0, 1);
                element.Play();
            };
            _music.MediaFailed += (o, args) =>
            {
                Logger.Current.Log("Mediaplayer failed.");
            };
            grid.Children.Add(_music);
            _music.Play();

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

                LoadOut.Completed += (o, args) =>
                {
                    var nav = Frame.Navigate(typeof(MainPage));

                };
                LoadOut.Begin();

            }
        }
    }
}
