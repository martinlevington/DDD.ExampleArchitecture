using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Controls;
using DDD.Wpf.Common.Exceptions;
using DDD.Wpf.Common.Windows;
using DryIoc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;

namespace DDD.Wpf.Common
{
    public abstract class WpfBootstrapper :  IBootstrapperModules
    {
        private static IContainer Container { get; set; }

        public void Run()
        {

        }

        public abstract IEnumerable<(Type, Type)> GetModuleTypes();



        protected IContainer CreateContainer()
        {

            Container.Register<IWindowService, WindowService>();

            foreach (var item in GetModuleTypes())
            {
                Type itemInterface= item.Item1;
                var itemType = item.Item2;


                Container.Register(
                    serviceType: itemInterface, 
                    implementationType: itemType,
                    ifAlreadyRegistered: IfAlreadyRegistered.Replace,
                    setup: Setup.With(asResolutionCall: true));
            }

            return Container;
        }


        protected  void ConfigureViewModelLocator()
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory(ResolveViewModel);
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(GetViewModel);
        }

        private object ResolveViewModel(object arg1, Type arg2)
        {
            var windowService = Container.Resolve<IWindowService>();

            using (var scope = Container.OpenScope())
            {
                var regionManager = windowService?.GetCurrentRegionManager();

                if (regionManager == null)
                {
                    return Container.Resolve(arg2);
                }
 
                scope.UseInstance(regionManager); // Scoped
               return  scope.Resolve(arg2);
            }

      
        }


        protected Type GetViewModel(Type viewType)
        {
            var name = viewType.FullName;

            // Step 1: try loading from the same directory
            var viewModel = name + "Model";
            var type = viewType.Assembly.GetType(viewModel, false);
            if (type != null) return type;

            // Step 2: try loading form a sibling directory named 'ViewModels'
            viewModel = name.Replace(".Views.", ".ViewModels.") + "Model";
            type = viewType.Assembly.GetType(viewModel, false); 
            if (type != null) return type;

            throw new ViewModelNotFoundException(viewType);
        }

    }
}
