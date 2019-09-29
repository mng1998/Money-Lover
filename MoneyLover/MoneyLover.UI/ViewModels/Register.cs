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
        private Views.SignIn signInView;
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

                    // MessageBox.Show("Đăng kí thành công!", "Thông Báo", MessageBoxImage.Information);
                 }
                 else
                 {
                     MessageBox.Show("Nhập sai định dạng email!","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                 }
                 
             };

            registerView.btnBack.Click += (sender, e) =>
            {
                registerView.Close();
                mainwindowView = new Views.MainWindow();
                mainwindowView.Show();
            };

            registerView.btnsignIn.Click += (sender, e) =>
            {
                registerView.Close();
                signInView = new Views.SignIn();
                signInView.Show();
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
