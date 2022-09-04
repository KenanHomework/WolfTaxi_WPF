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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.ViewModels;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.Views.Pages
{
    /// <summary>
    /// Interaction logic for AdjustmentPage.xaml
    /// </summary>
    public partial class AdjustmentPage : Page
    {
        public AdjustmentPage()
        {
            InitializeComponent();
            TaxiTypeComboBox.ItemsSource = Enum.GetValues(typeof(TaxiTypes));
        }

        private void TaxiTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaxiIcon.ImageSource = BitmapService.GetBitmapImageFromUrl(TaxiTypeComboBox.SelectedItem.ToString());
        }
    }
}
