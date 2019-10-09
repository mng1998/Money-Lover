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
        public Views.Register registerView;
        private SignIn signIn;

        public Register(MainWindow mainWindow)
        {
            registerView = new Views.Register();

            registerView.btnRegister.Click += (sender, e) =>
             {
                 if (Register(registerView.txtEmail.Text, registerView.psdPassword.Password))
                 {
                     MessageBox.Show("Đăng kí thành công!", "Thông Báo", MessageBoxButton.OK ,MessageBoxImage.Information);

                     registerView.Hide();
                     signIn = new SignIn(mainWindow);
                     signIn.signinView.Show();
                 }
             };

            registerView.btnBack.Click += (sender, e) =>
            {
                registerView.Hide();
                mainWindow.mainWindow.Show();
            };

            registerView.btnsignIn.Click += (sender, e) =>
            {
                registerView.Hide();
                signIn = new SignIn(mainWindow);
                signIn.signinView.Show();
            };
        }

        public void Show()
        {
            registerView.Show();
        }
    }
}
