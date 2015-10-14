using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace emu_pi2.UI.Extensions
{
    public static class NavigationExtensions
    {

        public static void NavigateToWithTransition(this Frame frame, Type navigateto, Storyboard transition)
        {
            transition.Completed += (o, args) =>
            {
                frame.Navigate(navigateto);
            };
            transition.Begin();
        }
    }
}
