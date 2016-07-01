using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Windows;
using System.Windows.Threading;

namespace Azelea.Demostrator
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            Current.DispatcherUnhandledException += CurrentDispatcherUnhandledException;

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        private static void CurrentDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandleException(e.Exception);
        }

        private static void HandleException(Exception ex)
        {
            if (ex != null)
            {
                ExceptionPolicy.HandleException(ex, "Default Policy");
                MessageBox.Show("发生严重错误导致程序崩溃，请查看日志排查问题。", 
                    "严重错误", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }
    }
}
