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
        private Views.MainWindow mainWindow;
        private Register register;
        private SignIn signin;

        public MainWindow()
        {
            mainWindow = new Views.MainWindow();

            mainWindow.btnRegister.Click += (sender, e) =>
            {
                register = new Register();
                register.Show();
            };

            mainWindow.btnSignIn.Click += (sender, e) =>
            {
                signin = new SignIn();
                signin.Show();
            };
        }

        public void Show()
        {
            mainWindow.Show();
        }
    }
}
