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

        public Location Location { get; set; } = new Location();

        public AddressComboBoxItemInfo(string name, string kind, Location location)
        {
            Name = name;
            Kind = kind;
            Location = location;
        }

        public AddressComboBoxItemInfo(string name, string kind)
        {
            Name = name;
            Kind = kind;
        }

        public AddressComboBoxItemInfo()
        {

        }

        public AddressComboBoxItemInfo(string name,Location location)
        {
            Name = name;
            Location = location;
        }

        public override string ToString() => Name;
    }
}
