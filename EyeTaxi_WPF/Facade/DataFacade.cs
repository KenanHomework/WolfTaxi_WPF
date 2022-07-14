using EyeTaxi_WPF.Enums;
using EyeTaxi_WPF.MVVM.Models.DerivedClasses;
using EyeTaxi_WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi_WPF.Facade
{
    public class DataFacade
    {

        #region Members

        public User User { get; set; } = new();

        public List<Driver> Drivers { get; set; } = new();

        public bool Remember { get; set; } = false;

        #endregion

        #region Methods

        public void ReadData(DataFacade dataFacade)
        {
            this.User = dataFacade.User;
            this.Drivers = dataFacade.Drivers;
            this.Remember = dataFacade.Remember;
        }

        public void Save()
        {
            JSONService.Write("dataset/dataFacade.json", this);
        }

        public void Load()
        {

            DataFacade loaded = this;

            try
            {
                loaded = JSONService.Read<DataFacade>("dataset/dataFacade.json");
            }
            catch (Exception) { }

            ReadData(loaded);

        }

        public void Exit()
        {
            User = null;
            Save();
        }

        public SearchProcessResult Login(User user)
        {

            SearchProcessResult result = HumanService.Search(user);
            if (result == SearchProcessResult.Success)
                User = user;

            return result;
        }

        public SearchProcessResult Signin(User user, bool autoSaveUser = true)
        {
            SearchProcessResult result = HumanService.Search(user);
            if (result == SearchProcessResult.NotFound)
            {
                User = user;
                if (autoSaveUser)
                    HumanService.Writehuman(User);

            }

            return SearchProcessResult.Success;
        }

        #endregion

        public override string ToString() => $"User: {User} \n Drivers:\n{Drivers.Count} \n Remember: {Remember}";
    }
}
