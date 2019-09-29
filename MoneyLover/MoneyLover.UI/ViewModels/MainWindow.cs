using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MoneyLover.UI.ViewModels
{
    public class MainWindow
    {
        private Views.MainWindow mainwindowView;

        public MainWindow()
        {
            mainwindowView = new Views.MainWindow();
        }

        public void Show()
        {
            mainwindowView.Show();
        }
    }
}
