using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.MVVM.Models.GeneralClasses
{
    public class AddressComboBoxItemInfo
    {
        public string Name { get; set; } = string.Empty;

        public string Kind { get; set; } = "History";
        public AddressComboBoxItemInfo(string name, string kind)
        {
            Name = name;
            Kind = kind;
        }

        public AddressComboBoxItemInfo()
        {

        }

        public AddressComboBoxItemInfo(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}
