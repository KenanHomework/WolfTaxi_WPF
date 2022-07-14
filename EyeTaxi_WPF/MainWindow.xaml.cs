using EyeTaxi_WPF.Facade;
using EyeTaxi_WPF.MVVM.Models.DerivedClasses;
using EyeTaxi_WPF.MVVM.Models.GeneralClasses;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace EyeTaxi_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //DataFacade data = new();
            //data.User = new User("kenan", new Hash("ysbv"), "kenanysbv@gmail.com", "055-555-55-55");
            //data.Drivers = new List<Driver>() {
            //    new Driver("kamil",new("Hello Kitty"),"filankes@nese.com","055-000-00-00", new(),new Taxi("Toyoto Prius",2015,"99-pr-999",ConsoleColor.Gray)),
            //    new Driver("cavid",new("cavidsi"),"cav@siz.com","055-000-00-00", new(),new Taxi("vaz 06",1994,"99-cc-999",ConsoleColor.Magenta))
            //};
            //data.Remember = true;
            //data.Load();
            //MessageBox.Show(data.ToString());
        }
    }
}
