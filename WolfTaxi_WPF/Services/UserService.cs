using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.Models.BaseClasses;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using System;
using System.IO;

namespace WolfTaxi_WPF.Services
{
    public abstract class UserService
    {

        public static ProcessResult Search(User user)
        {
            if (!File.Exists(user.GetPath()))
                return ProcessResult.NotFound;

            User data;
            try { data = Read(user); }
            catch (Exception) { return ProcessResult.Empty; }

            if (data == null)
                return ProcessResult.NotFound;

            if (data.Password.Compare(user.Password))
                return ProcessResult.Success;

            return ProcessResult.IncorrectPassword;
        }

        public static ProcessResult Search(string userame, string password)
        {

            if (!File.Exists($"{App.UserSubFilePath}/{userame}.json"))
                return ProcessResult.NotFound;

            User data;

            try { data = Read(userame); }
            catch (Exception) { return ProcessResult.Empty; }

            if (data == null)
                return ProcessResult.NotFound;

            if (data.Password.Compare(password))
                return ProcessResult.Success;

            return ProcessResult.IncorrectPassword;
        }

        public static User Read(User user) => JSONService.Read<User>(user.GetPath());

        public static User Read(string username) => JSONService.Read<User>($"{App.UserSubFilePath}/{username}.json");

        public static void Write(User user) => JSONService.Write(user.GetPath(), user);

    }
}
