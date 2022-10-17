using WolfTaxi_WPF.MVVM.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfTaxi_WPF.Enums;

namespace WolfTaxi_WPF.MVVM.Models.DerivedClasses
{
    public class FastTaxiType : TaxiTypeBase
    {
        public FastTaxiType()
            : base("Fast", "https://res.cloudinary.com/kysbv/image/upload/v1658306801/WolfTaxi/taxi_type_fast.png", TaxiTypes.Fast, 2f) { }
    }
}
