using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Wpf.Common.Data
{
    public interface IResultDataCollection
    {
        IEnumerable<IResultData> GetResults();
    }
}
