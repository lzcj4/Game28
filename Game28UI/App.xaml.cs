using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Game28UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            //var catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            //CompositionContainer container = new CompositionContainer(catalog);
            //container.ComposeParts(this);
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Debug.WriteLine(e.Exception.Message);
        }
    }
}
