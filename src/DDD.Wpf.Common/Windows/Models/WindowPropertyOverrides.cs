﻿using System.Windows;

namespace DDD.Wpf.Common.Windows.Models
{
    public class WindowPropertyOverrides
    {
        public SizeToContent? SizeToContent { get; set; }

        public string Title { get; set; }

        public ResizeMode? ResizeMode { get; set; }

        public string IconPath { get; set; }

        public WindowStyle? WindowStyle { get; set; }

        public bool? CloseOnDeactivate { get; set; }

        public bool? ShowInTaskbar { get; set; }

        public WindowState? WindowState { get; set; }

        public bool ExitApplicationOnClose { get; set; }
    }
}