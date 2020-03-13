using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using DDD.Wpf.Common.Windows;
using DDD.Wpf.Common.Windows.Events;
using Prism.Events;
using Prism.Regions;
namespace DDD.Wpf.Common.Application
{
    public class ApplicationShutdownService : IApplicationShutdownService

    {
    private readonly IWindowService _windowService;
    private readonly List<string> _unsavedWindows = new List<string>();
    private bool _isTerminating;

    public ApplicationShutdownService(IEventAggregator eventAggregator, IWindowService windowService)
    {
        _windowService = windowService;

        eventAggregator.GetEvent<AllWindowsClosedEvent>().Subscribe(AfterAllWindowsClosed);
    }

    public List<string> GetBlockingWindows()
    {
        return _unsavedWindows;
    }

    public bool ExitApplication(bool forceExit)
    {
        _isTerminating = true;
        _unsavedWindows.Clear();

        if (forceExit)
        {
            foreach (var window in _windowService.GetWindows())
            {
                window.IsClosingByForce = true;
            }
        }
        else if (ApplicationHasUnsavedChanges())
        {
            _isTerminating = false;

            _windowService.Open(nameof(Windows.Views.UnsavedChangesView), 1);

            return false;
        }

        foreach (var window in _windowService.GetWindows())
        {
            _windowService.CloseWindow(window.Key);
        }

        return true;
    }

    private bool ApplicationHasUnsavedChanges()
    {
        var hasUnsavedChanges = false;

        foreach (var window in _windowService.GetWindows())
        {
            var windowHasUnsavedChanges = window.HasUnsavedChanges();

            if (windowHasUnsavedChanges)
            {
                _unsavedWindows.Add(window.Window.Title);
            }

            hasUnsavedChanges = hasUnsavedChanges || windowHasUnsavedChanges;
        }

        return hasUnsavedChanges;
    }

    private void AfterAllWindowsClosed()
    {
        if (_isTerminating)
        {
            // all the windows where closed
            System.Windows.Application.Current.Shutdown();
        }
    }
    }
}
