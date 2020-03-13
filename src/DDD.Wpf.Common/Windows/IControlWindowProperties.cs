using DDD.Wpf.Common.Windows.Models;

namespace DDD.Wpf.Common.Windows
{
    public interface IControlWindowProperties
    {
        event WindowPropertiesChangedDelegate WindowPropertiesChanged;

        WindowPropertyOverrides GetWindowPropertyOverrides();
    }

    public delegate void WindowPropertiesChangedDelegate(IControlWindowProperties sender);
}
