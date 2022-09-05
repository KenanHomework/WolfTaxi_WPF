using WolfTaxi_WPF.Interfaces;
using WolfTaxi_WPF.MVVM.ViewModels;
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
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Media;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window, IResetable
    {
        public AdminPanel()
        {
            InitializeComponent();
            DataContext = App.DataFacade;
        }

        #region Members

        public bool State { get; set; } = false;

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

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
                        Application.Current.Shutdown();
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

        public void Reset()
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!State && DriverListView.SelectedIndex >= 0)
            {
                EditDriver editDriver = new();
                App.Container.GetInstance<EditDriverVM>().Driver = (Driver)DriverListView.SelectedItem;
                editDriver.ShowDialog();
            }
            else
                DriverListView.SelectedItems.Add(DriverListView.SelectedItem);

        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            App.ToEnterWindow();
        }

        public void ReadyForDeleting()
        {
            InfoText.Foreground = new SolidColorBrush(State ? Color.FromRgb(244, 164, 96) : Color.FromRgb(0, 255, 255));
            InfoText.Text = State ? "Plese Select Driver(s) for delete." : "Select Driver(s) for view / edit";
            DeleteSatateButton.IsEnabled = !State;
            Add.Visibility = State ? Visibility.Collapsed : Visibility.Visible;
            Cancel.Visibility = Delete.Visibility = State ? Visibility.Visible : Visibility.Collapsed;
            if (DriverListView.SelectionMode == SelectionMode.Extended) DriverListView.SelectedItems.Clear();
            DriverListView.SelectionMode = State ? SelectionMode.Extended : SelectionMode.Single;
        }

        private void DeleteDrivers_Click(object sender, RoutedEventArgs e)
        {
            State = true;
            ReadyForDeleting();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            State = false;
            ReadyForDeleting();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            foreach (Driver item in DriverListView.SelectedItems)
            {
                try { App.DataFacade.DeleteDriver(item); }
                catch (Exception) { }
            }
            State = false;
            ReadyForDeleting();
            SoundService.Notification();
            RefreshDriversListViewSource();
        }

        public void RefreshDriversListViewSource()
        {
            DriverListView.ItemsSource = null;
            DriverListView.ItemsSource = App.DataFacade.Drivers;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddDriver addDriver = new();
            addDriver.ShowDialog();
            RefreshDriversListViewSource();
        }
    }
}
