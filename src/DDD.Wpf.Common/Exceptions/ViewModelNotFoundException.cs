using System;

namespace DDD.Wpf.Common.Exceptions
{
    public class ViewModelNotFoundException : Exception
    {
        public ViewModelNotFoundException(Type viewType)
            : base("Viewmodel could not be located for the type " + viewType.FullName)
        {
        }
    }
}