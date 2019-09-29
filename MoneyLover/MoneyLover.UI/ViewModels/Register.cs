using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MoneyLover.UI.Models;

namespace MoneyLover.UI.ViewModels
{
    public class Register : Services.AccountService
    {
        private Views.Register registerView;
        private Views.MainWindow mainwindowView;

        public Register()
        {
            registerView = new Views.Register();

            registerView.btnRegister.Click += (sender, e) =>
             {
                 if (Register(registerView.txtEmail.Text, registerView.psdPassword.Password))
                 {
                     MessageBox.Show("Register Completed !");
                 }
             };

            registerView.btnBack.Click += (sender, e) =>
            {
                registerView.Close();
                mainwindowView = new Views.MainWindow();
                mainwindowView.Show();
            };

            registerView.btnExit.Click += (sender, e) =>
            {
                Application.Current.Shutdown();
            };
        }

        public void Show()
        {
            registerView.Show();
        }
    }
}
