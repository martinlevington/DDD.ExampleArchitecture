using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Wpf.Common.Data
{
    public class ResultData : IResultData
    {
        public string RegionName { get; set; }

        public object[] Results { get; set; }

        public ResultData(string regionName, object[] results)
        {
            RegionName = regionName;
            Results = results;
        }
    }
}
