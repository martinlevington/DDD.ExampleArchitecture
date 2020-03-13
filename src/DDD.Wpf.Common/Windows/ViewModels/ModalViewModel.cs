using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using DDD.Wpf.Common.Windows.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace DDD.Wpf.Common.Windows.ViewModels
{
   public class ModalViewModel : BindableBase, INavigationAware, IControlWindowProperties, IHasResults
    {
        private readonly IWindowService _windowService;
        public event WindowPropertiesChangedDelegate WindowPropertiesChanged;

        private string _iconPath;
        private bool _hasYesButton;
        private bool _hasNoButton;
        private bool _hasOkButton;
        private bool _hasCancelButton;
        private string _title;
        private string _message;
        private string _imagePath;
        private bool _hasCustomIcon;
        private ModalButton? _windowResult;

        public string IconPath
        {
            get { return _iconPath; }
            set
            {
                _iconPath = value; 
                RaisePropertyChanged();
            }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                _imagePath = value; 
                RaisePropertyChanged();
            }
        }

        public bool HasCustomIcon
        {
            get { return _hasCustomIcon; }
            set
            {
                _hasCustomIcon = value; 
                RaisePropertyChanged();
            }
        }

        public bool HasYesButton
        {
            get { return _hasYesButton; }
            set
            {
                _hasYesButton = value; 
                RaisePropertyChanged();
            }
        }

        public bool HasNoButton
        {
            get { return _hasNoButton; }
            set
            {
                _hasNoButton = value; 
                RaisePropertyChanged();
            }
        }

        public bool HasOkButton
        {
            get { return _hasOkButton; }
            set
            {
                _hasOkButton = value; 
                RaisePropertyChanged();
            }
        }

        public bool HasCancelButton
        {
            get { return _hasCancelButton; }
            set
            {
                _hasCancelButton = value; 
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value; 
                RaisePropertyChanged();
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value; 
                RaisePropertyChanged();
            }
        }

        public ICommand CloseCommand => new DelegateCommand<ModalButton?>(Close);

        public ModalViewModel(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Title = navigationContext.Parameters[nameof(Title)] as string;
            Message = navigationContext.Parameters[nameof(Message)] as string;

            var modalIcon = (ModalIcon)navigationContext.Parameters[nameof(ModalIcon)];
            HasCustomIcon = modalIcon != ModalIcon.None;

            if (HasCustomIcon)
            {
                IconPath = GetIconPath(modalIcon, "ico");
                ImagePath = GetIconPath(modalIcon, "png");
            }



            var modalButtons = (List<ModalButton>) navigationContext.Parameters[nameof(ModalButton)] ;
            HasYesButton = modalButtons.Contains(ModalButton.Yes);
            HasNoButton = modalButtons.Contains(ModalButton.No);
            HasOkButton = modalButtons.Contains(ModalButton.Ok);
            HasCancelButton = modalButtons.Contains(ModalButton.Cancel);

            WindowPropertiesChanged?.Invoke(this);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public WindowPropertyOverrides GetWindowPropertyOverrides()
        {
            return new WindowPropertyOverrides()
            {
                Title = Title,
                IconPath = HasCustomIcon ? IconPath : null,
                ResizeMode = ResizeMode.NoResize,
                SizeToContent = SizeToContent.WidthAndHeight,
                ShowInTaskbar = false
            };
        }

        public object[] GetResults()
        {
            return new object[] { _windowResult };
        }

        private string GetIconPath(ModalIcon modalIcon, string suffix)
        {
            const string basePath = "pack://application:,,,/DDD.Wpf.Common;component/Resources/{0}.{1}";
            string iconPath = "";

            switch (modalIcon)
            {
                case ModalIcon.None:
                    break;
                case ModalIcon.Question:
                    iconPath = "DialogQuestion";
                    break;
                case ModalIcon.Information:
                    iconPath = "DialogInfo";
                    break;
                case ModalIcon.Warning:
                    iconPath = "DialogWarning";
                    break;
                case ModalIcon.Error:
                    iconPath = "DialogError";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return string.Format(basePath, iconPath, suffix);
        }

        private void Close(ModalButton? result)
        {
            _windowResult = result;
            _windowService.CloseContainingWindow(this);
        }
    }
}
