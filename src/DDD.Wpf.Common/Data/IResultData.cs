using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Wpf.Common.Data
{
    public interface IResultData
    {
        string RegionName { get; }

        object[] Results { get; }
    }
}
