using Azelea.Demostrator.Infrastructure;
using Azelea.Demostrator.Module.Navigation.Views;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System.ComponentModel.Composition;

namespace Azelea.Demostrator.Module.Navigation
{
    [ModuleExport(typeof(NavigationModule))]
    public class NavigationModule : IModule
    {
        [Import]
        private IRegionManager _RegionManager = null;

        public void Initialize()
        {
            _RegionManager.RegisterViewWithRegion(RegionName.NavigationRegion, typeof(NavigationView));
        }
    }
}
