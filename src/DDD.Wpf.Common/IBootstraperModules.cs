using System;
using System.Collections.Generic;

namespace DDD.Wpf.Common
{
    public interface IBootstrapperModules
    {

        IEnumerable<(Type, Type)> GetModuleTypes();


    }
}
