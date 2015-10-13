using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace emu_pi2.UI.Extensions
{
    public static class DependencyObjectExtensions
    {
        public static T FindChildByName<T>(this DependencyObject depObj, string childName)
            where T : DependencyObject
        {
            if (depObj == null)
                return null;

            if (depObj is T && ((FrameworkElement)depObj).Name == childName)
                return depObj as T;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                T obj = FindChildByName<T>(child, childName);

                if (obj != null)
                    return obj;
            }

            return null;
        }
        public static T FindChildByType<T>(this DependencyObject depObj)
            where T : DependencyObject
        {
            if (depObj == null)
                return null;

            if (depObj is T && (depObj.GetType() == typeof(T)))
                return depObj as T;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                T obj = FindChildByType<T>(child);

                if (obj != null)
                    return obj;
            }

            return null;
        }
    }
}
