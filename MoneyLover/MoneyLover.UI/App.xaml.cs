using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DB.MoneyLoverSeedData seed = new DB.MoneyLoverSeedData();
            ViewModels.MainWindow mainWindow = new ViewModels.MainWindow();
            mainWindow.Show();
        }
    }
}
