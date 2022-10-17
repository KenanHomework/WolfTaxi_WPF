using WolfTaxi_WPF.MVVM.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.MVVM.Models.DerivedClasses
{
    public class ComfortTaxiType : TaxiTypeBase
    {
        public ComfortTaxiType()
            : base("Comfort", "https://res.cloudinary.com/kysbv/image/upload/v1658306801/WolfTaxi/taxi_type_comfort.png", Enums.TaxiTypes.Comfort, 1.2f) { }
    }
}
