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
        private Views.SignIn signInView;
        private Views.MainWindow mainwindowView;

        public Register()
        {
            registerView = new Views.Register();

            registerView.btnRegister.Click += (sender, e) =>
             {
                 if (Register(registerView.txtEmail.Text, registerView.psdPassword.Password))
                 {
<<<<<<< HEAD
                     MessageBox.Show("Đăng kí thành công!", "Thông Báo");
=======
                     MessageBox.Show("Đăng kí thành công!", "Thông Báo", MessageBoxButton.OK ,MessageBoxImage.Information);
>>>>>>> fabc42f8d92a06d1bc7c123620cbf339866b5c6b
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
    }
}
