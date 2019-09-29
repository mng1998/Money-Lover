using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MoneyLover.UI.Models;

namespace MoneyLover.UI.ViewModels
{
    public class SignIn
    {
        private Views.SignIn signinView;
        private Views.Register registerView;
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

            signinView.btnRegister.Click += (sender, e) =>
            {
                signinView.Close();
                registerView = new Views.Register();
                registerView.Show();
            };

            signinView.btnSignIn.Click += (sender, e) =>
            {
                using (var db = new DB.MoneyLoverDB())
                {
                    string email = signinView.txtEmail.Text;
                    User user = db.Users.Where(m => m.Email == email).SingleOrDefault();
                    if (user != null)
                    {
                        if (user.Password == signinView.psdPassword.Password)
                        {
                            MessageBox.Show("Login Success!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Email or Password is not correct!");
                    }
                }
            };
        }

        public void Show()
        {
            signinView.Show();
        }
    }
}
