using WolfTaxi_WPF.MVVM.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WolfTaxi_WPF.Enums;

namespace WolfTaxi_WPF.MVVM.Models.DerivedClasses
{
    public class LuxTaxiType : TaxiTypeBase
    {
        public LuxTaxiType()
            : base("Lux", "https://res.cloudinary.com/kysbv/image/upload/v1658306801/WolfTaxi/taxi_type_lux.png", TaxiTypes.Lux, 4f) { }
    }
}
