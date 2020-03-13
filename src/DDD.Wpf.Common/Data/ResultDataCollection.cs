using System.Collections.Generic;

namespace DDD.Wpf.Common.Data
{
    public class ResultDataCollection : IResultDataCollection
    {
        private readonly List<ResultData> _results = new List<ResultData>();

        public void AddResult(string viewName, object[] data)
        {
            _results.Add(new ResultData(viewName, data));
        }

        public IEnumerable<IResultData> GetResults()
        {
            return _results;
        }
    }
}
