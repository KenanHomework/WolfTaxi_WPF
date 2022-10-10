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
using CloudinaryDotNet;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CloudinaryDotNet.Actions;
using WolfTaxi_WPF.MVVM.ViewModels;
using WolfTaxi_WPF.Enums;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WolfTaxi_WPF.Services;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {

        public AddDriver()
        {
            InitializeComponent();
            App.Container.GetInstance<AddDriverVM>().Reset();
            App.Container.GetInstance<AddDriverVM>().Window = this;
            DataContext = App.Container.GetInstance<AddDriverVM>();
            TaxiTypeComboBox.ItemsSource = Enum.GetValues(typeof(TaxiTypes));
        }

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Controls Checks

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
                        => RegxService.CheckControl(ref Username, 4, Color.FromRgb(237, 236, 239), "(-?([A-Z].\\s)?([A-Z][a-z]+)\\s?)+([A-Z]'([A-Z][a-z]+))?");


        private void Email_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Email, 3, Color.FromRgb(179, 179, 179), "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b");


        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Phone, 10, Color.FromRgb(179, 179, 179), "^((\\050|055|099|077|070|010|051)|0)\\s?(4(099|055|050|077|070|010)\\d{1}|\\d{1})\\/?(((\\s?|\\.?)\\d{2}){3}|((\\s?|\\.)\\d{3}){2})\\b");

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegxService.CheckControl(ref Password, 8, Color.FromRgb(179, 179, 179), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            App.Container.GetInstance<AddDriverVM>().PasswordTemp = Password.Password;
        }

        private void Fin_TextChanged(object sender, RoutedEventArgs e)
        {
            RegxService.CheckControl(ref Fin, 7, Color.FromRgb(179, 179, 179), "(([a-zA-Z0-9]).{6})");
            App.Container.GetInstance<AddDriverVM>().FinCode = Fin.Text.ToUpper();
        }

        private void Locatoion_TextChanged(object sender, TextChangedEventArgs e)
                         => RegxService.CheckControl(ref Location, 3, Color.FromRgb(179, 179, 179));

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
                           => RegxService.CheckControl(ref Model, 3, Color.FromRgb(179, 179, 179));

        private void Year_TextChanged(object sender, TextChangedEventArgs e)
                           => RegxService.CheckCarYear(ref Year, Color.FromRgb(179, 179, 179));

        private void Number_TextChanged(object sender, TextChangedEventArgs e)
                         => RegxService.CheckControl(ref Number, 7, Color.FromRgb(179, 179, 179));

        #endregion

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Content.ToString())
                {
                    case "_":
                        this.WindowState = WindowState.Minimized;
                        break;
                    case "X":
                        App.DataFacade.Save();
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void TaxiTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaxiIcon.ImageSource = BitmapService.GetBitmapImageFromUrl($"https://res.cloudinary.com/kysbv/image/upload/v1658306801/WolfTaxi/taxi_type_{TaxiTypeComboBox.SelectedItem.ToString().ToLower()}.png");
        }

    }
}
