using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MoneyLover.UI.Models;

namespace MoneyLover.UI.ViewModels
{
    public class SignIn : Services.AccountService
    {
        private Views.SignIn signinView;
        private Views.MainWindow mainwindowView;

        public SignIn()
        {
            signinView = new Views.SignIn();

            signinView.btnBack.Click += (sender, e) =>
            {
                signinView.Close();
                mainwindowView = new Views.MainWindow();
                mainwindowView.Show();
            };

            signinView.btnExit.Click += (sender, e) =>
            {
                Application.Current.Shutdown();
            };

            signinView.btnSignIn.Click += (sender, e) =>
            {
                if (Login(signinView.txtEmail.Text, signinView.psdPassword.Password))
                    MessageBox.Show("Login Success!");
                else
                    MessageBox.Show("Email or Password is not correct!");
            };
        }

        public void Show()
        {
            signinView.Show();
        }
    }
}
