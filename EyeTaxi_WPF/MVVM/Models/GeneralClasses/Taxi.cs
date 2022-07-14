using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi_WPF.MVVM.Models.GeneralClasses
{
    public class Taxi
    {

        #region Members

        public string Model { get; set; } = string.Empty;

        public int Year { get; set; } = 1970;

        public string Number { get; set; } = String.Empty;

        public ConsoleColor Color { get; set; } = ConsoleColor.White;

        #endregion

        public Taxi() { }

        public Taxi(string model, int year, string number, ConsoleColor color)
        {
            Model = model;
            Year = year;
            Number = number;
            Color = color;
        }
    }
}
