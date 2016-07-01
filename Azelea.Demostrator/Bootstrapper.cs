using Azelea.Demostrator.Views;
using Microsoft.Practices.Prism.Logging;
using Microsoft.Practices.Prism.MefExtensions;
using System;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Azelea.Demostrator
{
    public class Bootstrapper : MefBootstrapper
    {
        private readonly EnterpriseLibraryLoggerAdapter _Logger = new EnterpriseLibraryLoggerAdapter();

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));

            string[] files = Directory.GetFiles(Environment.CurrentDirectory, "Azelea.Demostrator.Module.*.dll");
            foreach (string file in files)
            {
                AggregateCatalog.Catalogs.Add(new AssemblyCatalog(Assembly.LoadFile(file)));
            }
        }

        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<ShellView>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (ShellView)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override ILoggerFacade CreateLogger()
        {
            return _Logger;
        }
    }
}
