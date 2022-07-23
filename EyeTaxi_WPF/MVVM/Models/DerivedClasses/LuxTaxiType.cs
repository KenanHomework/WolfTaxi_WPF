using EyeTaxi_WPF.MVVM.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi_WPF.MVVM.Models.DerivedClasses
{
    public class LuxTaxiType : TaxiTypeBase
    {

        public LuxTaxiType() : base(Enums.TaxiTypes.Lux, 2.5f, "https://res.cloudinary.com/kysbv/image/upload/v1658306801/EyeTaxi/taxi_type_lux.png") { }

    }
}
