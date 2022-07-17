﻿using EyeTaxi_WPF.Enums;
using EyeTaxi_WPF.MVVM.Models.BaseClasses;
using EyeTaxi_WPF.MVVM.Models.DerivedClasses;
using System;
using System.IO;

namespace EyeTaxi_WPF.Services
{
    public class UserService
    {

        public static ProcessResult Search(User user)
        {
            User data;
            try { data = Read(user); }
            catch (Exception) { return ProcessResult.Empty; }

            if (data == null)
                return ProcessResult.NotFound;

            if (data.Password.Compare(user.Password))
                return ProcessResult.Success;

            return ProcessResult.IncorrectPassword;
        }

        public static User Read(User user) => JSONService.Read<User>(user.GetPath());

        public static void Write(User user) => JSONService.Write(user.GetPath(), user);

    }
}
