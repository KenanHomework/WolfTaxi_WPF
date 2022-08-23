﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WolfTaxi_WPF.MVVM.ViewModels;

namespace WolfTaxi_WPF.Services
{
    public abstract class BitmapService
    {
        public static BitmapImage GetBitmapImageFromUrl(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path, UriKind.Absolute);
            bitmap.EndInit();

            return bitmap;
        }

    }
}
