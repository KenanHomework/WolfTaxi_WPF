using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WolfTaxi_WPF.Services
{
    public abstract class RegxService
    {
        public static bool CheckControl(ref TextBox tbx, int minLenght, Color defColor, string regex)
        {
            if (tbx.Text.Length >= minLenght && new Regex(regex).IsMatch(tbx.Text))
            {
                tbx.Foreground = new SolidColorBrush(defColor);
                return true;
            }
            tbx.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            return false;
        }

        public static bool CheckControl(ref PasswordBox tbx, int minLenght, Color defColor, string regex)
        {
            if (tbx.Password.Length >= minLenght && new Regex(regex).IsMatch(tbx.Password))
            {
                tbx.Foreground = new SolidColorBrush(defColor);
                return true;
            }
            tbx.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
            return false;
        }
    }
}
