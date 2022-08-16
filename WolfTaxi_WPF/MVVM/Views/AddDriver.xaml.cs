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
            DataContext = App.Container.GetInstance<AddDriverVM>();
            TaxiTypeComboBox.ItemsSource = Enum.GetValues(typeof(TaxiTypes));
        }

        private void BrowsePP_Click(object sender, RoutedEventArgs e)
        {
            //    var uploadParams = new ImageUploadParams()
            //    {
            //        File = new FileDescription(@"c:\my_image.jpg")
            //    };
            //    var uploadResult = App.cloudinary.Upload(uploadParams);
        }

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

        private void Add_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TaxiTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Container.GetInstance<AddDriverVM>().Driver.Taxi.Type = (TaxiTypes)TaxiTypeComboBox.SelectedItem;
            App.Container.GetInstance<AddDriverVM>().Driver.Taxi.IconSource = $"https://res.cloudinary.com/kysbv/image/upload/v1658306801/WolfTaxi/taxi_type_{TaxiTypeComboBox.SelectedItem.ToString().ToLower()}.png";
        }
    }
}
