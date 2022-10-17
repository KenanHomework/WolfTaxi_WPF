using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WolfTaxi_WPF.MVVM.Models.BaseClasses;
using System.Linq.Expressions;
using System.Xml;

namespace WolfTaxi_WPF.Facade
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
            if (User == null) return;
            UserService.Write(User);
        }

        public void Load()
        {

            DataFacade loaded = this;

            try
            {
                loaded = JSONService.Read<DataFacade>("dataset/dataFacade.json");
                ReadData(loaded);
            }
            catch (Exception) {  }



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

        public ProcessResult Login(User user)
        {

            ProcessResult result = UserService.Search(user);
            if (result == ProcessResult.Success)
                User = UserService.Read(user.Username);

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
            // Check Drivers List 
            if (Drivers.Count <= 0)
                Drivers = new();

            Drivers.ForEach(d =>
            {
                if (d.ID == driver.ID)
                {
                    d.Update(driver);
                    return;
                }
            });
        }

        public ProcessResult AddDriver(Driver driver)
        {
            if (DriverService.DriverExis(driver))
                return ProcessResult.Existed;

            drivers.Add(driver);
            return ProcessResult.Success;
        }

        public void DeleteDriver(Driver driver) => DeleteDriver(driver.ID);

        public void DeleteDriver(Guid ID) => Drivers.ForEach(d =>
        {
            if (d.ID == ID)
            {
                Drivers.Remove(d);
                try { CloudinaryService.DestroyImage(ID.ToString(), App.DriverCloudinaryFolderPath); }
                catch (Exception) { }
                return;
            }
        });

        public void DeleteDriver(IList<Driver> drivers) => drivers.ToList().ForEach(d =>
        {
            try { DeleteDriver(d.ID); }
            catch (Exception) { }
        });

        public float GetPrice(TaxiTypes type) => App.AllTaxiType.Find(t => t.Type == type).Price;

        public TaxiTypeBase GetTaxiTypeBase(TaxiTypes type) => App.AllTaxiType.Find(t => t.Type == type);

        public void UpdateTaxiTypePrice(TaxiTypes type, float price) => App.AllTaxiType.ForEach(t => { if (type == t.Type) t.Price = price; });

        #endregion

        public DataFacade() {  }

        public override string ToString() => $"User: {User}  \n Remember: {Remember}";
    }
}
