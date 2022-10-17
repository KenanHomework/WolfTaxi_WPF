using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.Models.BaseClasses;
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
            //TaxiTypeComboBox.ItemsSource = Enum.GetValues(typeof(TaxiTypes));
            TaxiTypeComboBox.ItemsSource = App.AllTaxiType;
            TaxiTypeComboBox.SelectedIndex = 0;
            DataContext = this;
            Save = new(SaveClick, CanSave);
        }

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Commands

        public RelayCommand Save { get; set; }

        #endregion

        private float price;

        public float PricePerKm { get => price; set { price = value; OnPropertyChanged(); } }

        private void TaxiTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //TaxiICon.ImageSource = BitmapService.GetBitmapImageFromUrl(App.DataFacade.GetTaxiTypeBase((TaxiTypes)TaxiTypeComboBox.SelectedItem).IconSource);
            //PricePerKm = App.DataFacade.GetTaxiTypeBase((TaxiTypes)TaxiTypeComboBox.SelectedItem).Price;
            //Price_TBX.Text = PricePerKm.ToString();         
            TaxiICon.ImageSource = BitmapService.GetBitmapImageFromUrl(((TaxiTypeBase)TaxiTypeComboBox.SelectedItem).IconSource);
            PricePerKm = ((TaxiTypeBase)TaxiTypeComboBox.SelectedItem).Price;
            Price_TBX.Text = PricePerKm.ToString();
        }

        public bool CanSave(object param) => PricePerKm != ((TaxiTypeBase)TaxiTypeComboBox.SelectedItem).Price;

        public void SaveClick(object param)
        {
            System.Windows.Forms.DialogResult result = CMessageBox.Show($"Taxi Type: {TaxiTypeComboBox.SelectedItem} \nPrice Per Km: {PricePerKm}\nDo you confirmation this price ?",
                CMessageTitle.Confirm, CMessageButton.Yes, CMessageButton.No);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                ((TaxiTypeBase)TaxiTypeComboBox.SelectedItem).Price = PricePerKm;
                App.DataFacade.Save();
                SoundService.Succes();
            }
        }

        private void Price_TBX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegxService.CheckTypePrice(ref Price_TBX, Colors.White))
                PricePerKm = Convert.ToSingle(Price_TBX.Text);

        }
    }
}
