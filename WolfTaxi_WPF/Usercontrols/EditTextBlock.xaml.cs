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

namespace WolfTaxi_WPF.Usercontrols
{
    /// <summary>
    /// Interaction logic for EditTextBlock.xaml
    /// </summary>
    public partial class EditTextBlock : UserControl
    {
        public EditTextBlock()
        {
            InitializeComponent();
        }

        public EditTextBlock(Binding text, string hit)
        {
            this.Value = text;
            this.Hit = hit;
        }

        #region Members

        private Binding text;

        public Binding Value
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        private string hit = "HitTest";

        public string Hit
        {
            get { return hit; }
            set { hit = value; OnPropertyChanged(); }
        }

        private bool isactive;

        public bool IsEditable
        {
            get { return isactive; }
            set { isactive = value; OnPropertyChanged(); }
        }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Methods

        public void PrepareEdit(bool edit)
        {
            SolidColorBrush infoColor = new SolidColorBrush(edit ? Color.FromRgb(179, 179, 179) : Color.FromRgb(96, 104, 108));
            HitLBL.Foreground = infoColor;
            Chevron.Foreground = infoColor;
            EditTBX.IsEnabled = edit;
        }

        #endregion
    }
}
