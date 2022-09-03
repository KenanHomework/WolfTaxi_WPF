using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.Interfaces;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.MVVM.ViewModels;
using WolfTaxi_WPF.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window, IResetable
    {
        public ForgotPassword(string username)
        {
            InitializeComponent();
            DataContext = new ForgotPasswordVM() { Username = username,Window = this};
        }

        public bool EnterInfo { get; set; } = true;

        public DialogResult DialogResult { get; set; } = DialogResult.Cancel;

        #region Methods

        private void ResizeButton_Click(object sender, RoutedEventArgs e) => this.Close();

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Email, 3, Color.FromRgb(237, 236, 239), "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b");

        public void Reset() => App.Container.GetInstance<ForgotPasswordVM>().Reset();

        #endregion
    }
}
