using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.Comparer
{
    public class Comparer : IEqualityComparer<IComparable>
    {
        public bool Equals(IComparable? x, IComparable? y)
        {
            return Convert.ToBoolean(x.CompareTo(y));
        }

        public int GetHashCode([DisallowNull] IComparable obj)
        {
            return obj.GetHashCode();
        }
    }
}
