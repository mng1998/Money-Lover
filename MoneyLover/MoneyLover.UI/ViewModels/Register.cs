using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MoneyLover.UI.Models;

namespace MoneyLover.UI.ViewModels
{
    public class Register
    {
        private Views.Register registerView;
        private Views.MainWindow mainwindowView;

        public Register()
        {
            registerView = new Views.Register();

            registerView.btnRegister.Click += (sender, e) =>
             {
                 string email = registerView.txtEmail.Text;
                 string passowrd = registerView.psdPassword.Password;

                 if(IsValidEmail(email))
                 {
                     using (var db = new DB.MoneyLoverDB())
                     {
                         db.Users.Add(new User { Email = email, Password = passowrd });
                         db.SaveChanges();
                     }

                     MessageBox.Show("Register Completed !");
                 }
                 else
                 {
                     MessageBox.Show("Email is invalid!");
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

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
