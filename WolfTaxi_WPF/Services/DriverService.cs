using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WolfTaxi_WPF.Exceptions;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;

namespace WolfTaxi_WPF.Services
{
    public abstract class DriverService
    {

        public static bool DriverExis(Driver driver, bool readID = false)
        {
            // Mean First Driver
            if (App.DataFacade.Drivers.Count == 0)
                return true;

            //
            if (App.DataFacade.Drivers.Count(s => s.Taxi.Number == driver.Taxi.Number && s.Phone == driver.Phone && s.Fin == driver.Fin && (readID ? s.ID == driver.ID : true)) == 0)
                return false;

            return false;

        }

        public static void SaveOrUpdateDriver(Driver driver, bool driverExisCaseChecked = false)
        {
            // Izah
            // True  -> Update Driver
            // False -> Save Driver
            bool conditionDriver;

            // Check Driver Unique Case
            if (!driverExisCaseChecked)
                conditionDriver = driverExisCaseChecked;
            else
                conditionDriver = DriverExis(driver, true);

            // Process
            if (conditionDriver)
                App.DataFacade.UpdateDriverInfo(driver);
            else
                App.DataFacade.AddDriver(driver);
        }

        public static void UpdateDriver(Driver driver, bool driverExisCaseChecked = false)
        {
            // Check Driver Exis Case
            if (!driverExisCaseChecked)
                if (!DriverExis(driver, true))
                    throw new ArgumentException(ExcCodes.DriverNotExisted.ToString());

            App.DataFacade.UpdateDriverInfo(driver);
        }

        public static void SaveDriver(Driver driver, bool driverExisCaseChecked = false)
        {

            // Check Driver Exis Case
            if (!driverExisCaseChecked)
                if (DriverExis(driver, true))
                    throw new ArgumentException(ExcCodes.DriverExisted.ToString());

            App.DataFacade.AddDriver(driver);
        }

    }
}
