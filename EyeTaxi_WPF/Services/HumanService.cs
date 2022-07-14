using EyeTaxi_WPF.Enums;
using EyeTaxi_WPF.MVVM.Models.BaseClasses;
using EyeTaxi_WPF.MVVM.Models.DerivedClasses;
using System;
using System.IO;

namespace EyeTaxi_WPF.Services
{
    public class HumanService
    {
        public static SearchProcessResult Search(string username, string password, string subFilePath)
        {
            Human human;
            try { human = ReadHuman(username, subFilePath); }
            catch (Exception) { return SearchProcessResult.Empty; }

            if (human == null)
                return SearchProcessResult.NotFound;

            if (human.Password.Compare(password))
                return SearchProcessResult.IncorrectPassword;

            return SearchProcessResult.Success;
        }

        public static SearchProcessResult Search(Human human)
        {
            try { human = ReadHuman(human.Username,human.SubFilePath); }
            catch (Exception) { return SearchProcessResult.Empty; }

            if (human == null)
                return SearchProcessResult.NotFound;

            if (human.Password != human.Password)
                return SearchProcessResult.IncorrectPassword;

            return SearchProcessResult.Success;
        }

        public static void Writehuman(Human human)
        {
            if (!Directory.Exists(human.SubFilePath))
                Directory.CreateDirectory(human.SubFilePath);
            JSONService.Write($"{human.SubFilePath}/{human.Username}.json", human);
        }

        public static Human ReadHuman(string username, string subFilePath)
        {
            if (!File.Exists($"{subFilePath}/{username}.json"))
                return null;

            try
            {
                return JSONService.Read<Human>($"{subFilePath}/{username}.json");
            }
            catch (Exception) { throw; }
        }
    }
}
