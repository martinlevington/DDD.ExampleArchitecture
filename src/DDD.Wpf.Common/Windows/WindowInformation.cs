using System;
using System.Windows;
using DDD.Wpf.Common.Data;
using Prism.Regions;

namespace DDD.Wpf.Common.Windows
{
    public class WindowInformation
    {
        public WindowInformation(WindowKey key, WindowKey parentKey, Window window,
            Action<IResultDataCollection> afterWindowClosedAction)
        {
            Key = key;
            ParentKey = parentKey;
            Window = window;
            AfterWindowClosedAction = afterWindowClosedAction;
        }

        public WindowKey Key { get; }

        public WindowKey ParentKey { get; }

        public Window Window { get; }

        public Action<IResultDataCollection> AfterWindowClosedAction { get; }

        public WindowKey DialogChildKey { get; set; }

        public bool CloseOnDeactivation { get; set; }

        public bool IsClosingByForce { get; set; }

        public bool StartedShutdownProcess { get; set; }

        public bool HasUnsavedChanges()
        {
            var windowHasUnsavedChanges = false;

            var windowAsEditor = Window as IEditor;
            if (windowAsEditor != null)
            {
                windowHasUnsavedChanges = windowAsEditor.HasUnsavedChanges();
            }

            var frameworkElement = Window as FrameworkElement;
            var dataContext = frameworkElement?.DataContext as IEditor;
            if (dataContext != null)
            {
                windowHasUnsavedChanges = windowHasUnsavedChanges || dataContext.HasUnsavedChanges();
            }

            windowHasUnsavedChanges = windowHasUnsavedChanges || ChildRegionsHaveUnsavedChanges();

            return windowHasUnsavedChanges;
        }

        private bool ChildRegionsHaveUnsavedChanges()
        {
            var manager = RegionManager.GetRegionManager(Window);

            if (manager != null)
            {
                foreach (var region in manager.Regions)
                {
                    foreach (var view in region.ActiveViews)
                    {
                        var viewEditor = view as IEditor;

                        if (viewEditor != null)
                        {
                            if (viewEditor.HasUnsavedChanges())
                            {
                                return true;
                            }
                        }

                        var frameworkElement = view as FrameworkElement;
                        var dataContext = frameworkElement?.DataContext as IEditor;

                        if (dataContext != null)
                        {
                            if (dataContext.HasUnsavedChanges())
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}