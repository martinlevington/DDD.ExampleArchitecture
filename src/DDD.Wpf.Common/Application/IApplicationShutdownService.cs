using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Wpf.Common.Application
{
    public interface IApplicationShutdownService
    {
        List<string> GetBlockingWindows();

        bool ExitApplication(bool forceExit);
    }
}
