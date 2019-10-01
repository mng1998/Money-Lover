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
        private Views.Register registerView;
        private Views.MainWindow mainwindowView;
        private PassbookList passBookList;

        public SignIn()
        {
            signinView = new Views.SignIn();

            signinView.btnBack.Click += (sender, e) =>
            {
                signinView.Close();
                mainwindowView = new Views.MainWindow();
                mainwindowView.Show();
            };

            signinView.btnRegister.Click += (sender, e) =>
            {
                signinView.Close();
                registerView = new Views.Register();
                registerView.Show();
            };

            signinView.btnSignIn.Click += (sender, e) =>
            {
                if (Login(signinView.txtEmail.Text, signinView.psdPassword.Password))
                {
                    signinView.Close();
                    passBookList = new PassbookList();
                    passBookList.Show();
                }
                else
                    MessageBox.Show("Email hoặc Mật khẩu không chính xác!");
            };
        }

        public void Show()
        {
            signinView.Show();
        }
    }
}
