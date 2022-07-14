using System;
using System.Collections.Generic;
using System.Linq;
using LoremNET;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi_WPF.MVVM.Models.GeneralClasses
{
    public class Hash
    {

        #region Members

        string[] SaltStrings = new string[2];
        char HashKey;
        string Value = string.Empty;

        #endregion

        #region Methods

        public string HashValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value can't be null or empty");

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

        public Hash()
        {
            HashKey = (char)new Random().Next(0, 255);

            SaltStrings[0] = Lorem.Words(1, 3);
            SaltStrings[1] = Lorem.Words(1, 3);
        }

        public Hash(string value) : this()
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value can't be null or empty");

            Value = HashValue(value);
        }


    }
}
