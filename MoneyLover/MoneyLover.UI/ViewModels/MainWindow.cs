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
        public Views.MainWindow mainWindow;
        private Register register;
        private SignIn signin;

        public MainWindow()
        {
            mainWindow = new Views.MainWindow();

            mainWindow.btnRegister.Click += (sender, e) =>
            {
                Hide();
                register = new Register(this);
                register.Show();
            };

            mainWindow.btnSignIn.Click += (sender, e) =>
            {
                Hide();
                signin = new SignIn(this);
                signin.Show();
            };

            mainWindow.btnExit.Click += (sender, e) =>
            {
                if (MessageBox.Show("Bạn muốn thoát ứng dụng ?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            };
        }

        public void Show()
        {
            mainWindow.Show();
        }

        public void Hide()
        {
            mainWindow.Hide();
        }
    }
}
