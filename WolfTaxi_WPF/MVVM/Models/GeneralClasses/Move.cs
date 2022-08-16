using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.MVVM.Models.GeneralClasses
{
    public class Move
    {

        #region Members

        public Location StartLocation { get; set; } = new();

        public Location EndLocation { get; set; } = new();

        public string StartTime { get; set; } = string.Empty;

        public string EndTime { get; set; } = string.Empty;

        public string MoveTime { get; set; } = string.Empty;

        public float Distance { get; set; } = 0;

        public float Price { get; set; } = 0;

        public User User { get; set; } = new();

        public Driver Driver { get; set; } = new();

        public int Rating { get; set; } = 0;

        #endregion

        public Move() { }

        public Move(Location startLocation, Location endLocation, string startTime, string endTime, string moveTime, float distance, float price, User user, Driver driver, int rating)
        {
            StartLocation = startLocation;
            EndLocation = endLocation;
            StartTime = startTime;
            EndTime = endTime;
            MoveTime = moveTime;
            Distance = distance;
            Price = price;
            User = user;
            Driver = driver;
            Rating = rating;
        }

    }
}
