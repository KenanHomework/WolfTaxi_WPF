﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.Usercontrols
{
    /// <summary>
    /// Interaction logic for SidebarItem.xaml
    /// </summary>
    public partial class SidebarItem : UserControl
    {

        public SidebarItem()
        {
            InitializeComponent();
            DataContext = this;
        }

        private string imageSource;

        public string ImageSource
        {
            get => imageSource;
            set => Img.Source = BitmapService.GetBitmapImageFromUrl(value);
        }

        public string Text { get; set; }

        public RelayCommand SidebarCommand { get; set; }
    }
}
