﻿using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.Interfaces;
using System;
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
using System.Windows.Shapes;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for EnterSecurityCode.xaml
    /// </summary>
    public partial class EnterSecurityCode : Window,IResetable
    {
        public EnterSecurityCode()
        {
            InitializeComponent();
        }

        public DialogResult DialogResult { get; set; } = DialogResult.Cancel;

        public int Code { get; set; } = -1;

        private void ResizeButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Submit(object sender, RoutedEventArgs e)
        {
            if (Code != Convert.ToInt32(SecurityCode.Text))
            {
                new MessageBoxCustom("Incorrect Security Code !", MessageType.Error, MessageButtons.Ok).ShowDialog();
                SecurityCode.Text = String.Empty;
            }
            else
            {
                DialogResult = DialogResult.Success;
                Close();
            }
        }

        private void SecurityCode_TextChanged(object sender, TextChangedEventArgs e)
            => Submite.IsEnabled = !string.IsNullOrWhiteSpace(SecurityCode.Text);

        public void Reset()
        {
            SecurityCode.Text = String.Empty;
        }
    }
}
