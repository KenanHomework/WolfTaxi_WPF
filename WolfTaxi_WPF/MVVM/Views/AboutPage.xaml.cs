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
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AboutPage.xaml
    /// </summary>
    public partial class AboutPage : Page
    {
        public AboutPage()
        {
            InitializeComponent();
            InfoText.Text = "This project is my C# WPF exam project.\r\nIn this project, I tried to develop as much as I could in accordance with the standards.\r\nThe project is written with MVVM and Design Patterns are applied.\r\nSome details about the project:\r\n* Storage: JSON\r\n* Image Cloud Service: Cloudinary\r\n* Map Service: Esri.ArcGISRuntime.WPF\r\nI have learnt a lot with this project.\r\nI will improve myself with projects like this from now on.\r\nSince I am in the learning phase, I may have many mistakes :).";
        }

        private void ProjectInfoImage_Click(object sender, RoutedEventArgs e) =>WebService.OpenWebsiteWithUrl("https://github.com/KenanHomework/WolfTaxi_WPF.git");

        private void HomeworkAccountInfoImage_Click(object sender, RoutedEventArgs e) => WebService.OpenWebsiteWithUrl("https://github.com/KenanHomework");

        private void AccountInfoImage_Click(object sender, RoutedEventArgs e) => WebService.OpenWebsiteWithUrl("https://github.com/kenanysbv");
    }
}
