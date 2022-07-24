using EyeTaxi_WPF.Enums;
using EyeTaxi_WPF.MVVM.Models.DerivedClasses;
using EyeTaxi_WPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi_WPF.Facade
{
    public class DataFacade
    {

        #region Members

        public User User { get; set; }

        public bool Remember { get; set; } = false;

        private List<Driver> drivers;
        public List<Driver> Drivers { get => drivers; set { drivers = value; OnPropertyChanged(); } }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Methods

        public void ReadData(DataFacade dataFacade)
        {
            this.User = dataFacade.User;
            this.Remember = dataFacade.Remember;
            this.Drivers = dataFacade.Drivers;
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
                ReadData(loaded);
            }
            catch (Exception) { }


        }

        public void Exit()
        {
            User = null;
            Save();
        }

        public ProcessResult Login(string username, string password)
        {

            ProcessResult result = UserService.Search(username, password);
            if (result == ProcessResult.Success)
                User = UserService.Read(username);

            return result;
        }

        public ProcessResult Signin(User user, bool autoSaveUser = true)
        {
            ProcessResult result = UserService.Search(user);
            if (result == ProcessResult.NotFound)
            {
                User = user;
                if (autoSaveUser)
                    UserService.Write(user);
            }

            return ProcessResult.Success;
        }

        public void UpdateDriverInfo(Driver driver)
        {
            if (Drivers.Contains(driver))
            {
                Drivers.ForEach(d =>
                {
                    if (d.ID == driver.ID)
                    {
                        d = new Driver(driver);
                        return;
                    }


                });
            }
        }

        #endregion

        public DataFacade() { }

        public override string ToString() => $"User: {User}  \n Remember: {Remember}";
    }
}
