using System;
using System.Collections.Generic;
using System.Text;
using DDD.Wpf.Common;

namespace DDD.WpfApp
{
    public class Bootstrapper : WpfBootstrapper
    {
        public override IEnumerable<(Type, Type)> GetModuleTypes()
        {
            yield return (typeof(IBootstrapperModules), typeof(Bootstrapper));
        }
    }
}
