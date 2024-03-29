﻿using System;
using System.Collections.Generic;
using System.Linq;
using LoremNET;
using System.Text;
using System.Threading.Tasks;
using WolfTaxi_WPF.Interfaces;
using System.Windows.Forms;

namespace WolfTaxi_WPF.MVVM.Models.GeneralClasses
{
    public class Hash : ICanCheckOwnMembers
    {

        #region Members

        public string[] SaltStrings { get; set; } = new string[2] { Lorem.Words(1, 3), Lorem.Words(1, 3) };

        public char HashKey { get; set; } = (char)new Random().Next(0, 255);

        public string Value { get; set; } = string.Empty;

        #endregion

        #region Methods

        public string HashValue(string value)
        {
            if (string.IsNullOrEmpty(value)) 
                throw new ArgumentNullException("Value can't be null !");

            StringBuilder sb = new();

            value += SaltStrings[0];
            foreach (char c in value)
                sb.Append(c ^ HashKey);

            value += SaltStrings[1];
            foreach (char c in value)
                sb.Append(c ^ HashKey);

            return sb.ToString();
        }

        public bool Compare(Hash other)
            => Value == other.Value;

        public bool Compare(string value)
            => Value == HashValue(value);

        public void UpdateValue(string value)
            => Value = HashValue(value);

        #endregion

        #region Implements

        public string AllMembersTrue()
            => string.IsNullOrWhiteSpace(Value) ? "Hash Password Value can't be empty" : "1";

        #endregion

        public Hash() { }

        public Hash(string value) : this()
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value can't be null or empty");

            Value = HashValue(value);
        }

        public Hash(Hash other)
        {
            this.Value = other.Value;
            this.HashKey = other.HashKey;
            this.SaltStrings = other.SaltStrings;
        }


    }
}
