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
        public Views.SignIn signinView;
        private PassbookList passBookList;
        private Register register;

        public SignIn(MainWindow mainWindow)
        {
            signinView = new Views.SignIn();

            signinView.btnBack.Click += (sender, e) =>
            {
                signinView.Hide();
                mainWindow.Show();
            };

            signinView.btnRegister.Click += (sender, e) =>
            {
                signinView.Hide();
                register = new Register(mainWindow);
                register.registerView.Show();
            };

            signinView.btnSignIn.Click += (sender, e) =>
            {
                if (Login(signinView.txtEmail.Text, signinView.psdPassword.Password))
                {
                    signinView.Close();
                    passBookList = new PassbookList(mainWindow);
                    passBookList.Show();
                }
                else
                    MessageBox.Show("Email hoặc Mật khẩu không chính xác!");
            };

            signinView.btnClose.Click += (sender, e) =>
            {
                if (MessageBox.Show("Bạn muốn thoát ứng dụng ?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Application.Current.Shutdown();
                }
            };
        }

        public void Show()
        {
            signinView.Show();
        }
    }
}
