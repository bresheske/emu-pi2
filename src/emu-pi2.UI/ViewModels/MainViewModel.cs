using emu_pi2.Data.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace emu_pi2.UI.ViewModels
{
    public class MainViewModel
    {
        public IEnumerable<Console> Consoles{ get; set; }
        public string Version { get; set; }
        public Console SelectedConsole { get; set; }
        public Frame LayoutRoot { get; set; }

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
    }
}
