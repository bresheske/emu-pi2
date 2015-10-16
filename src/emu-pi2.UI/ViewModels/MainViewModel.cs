using emu_pi2.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Core;

namespace emu_pi2.UI.ViewModels
{
    public class MainViewModel
    {

        public IEnumerable<Console> Consoles { get; set; }
        public IEnumerable<Game> Games { get; set; }

        public string Version { get; set; }
        public Console SelectedConsole { get; set; }
        public Game SelectedGame { get; set; }

        public Frame LayoutRoot { get; set; }
        public MediaElement SoundSelect { get; set; }

        private static MainViewModel _instance;


        private MainViewModel()
        {

        }

        public static MainViewModel Current
        {
            get
            {
                if (_instance == null)
                    _instance = new MainViewModel();
                return _instance;
            }
        }

        public CoreDispatcher Dispatcher { get; internal set; }
    }
}
