using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.Enums
{
    public enum ProcessResult { Success, NotFound, Empty, IncorrectPassword, Existed, FileError, Error, EmptyArguments }

    public enum DialogResult { Success, Cancel, Ok, No, Yes }

    public enum TaxiTypes { Comfort, Fast, Lux }

    public enum CMessageButton { Ok, Yes, No, Cancel, Confirm, None }

    public enum CMessageTitle { Error, Info, Warning, Confirm }

}
