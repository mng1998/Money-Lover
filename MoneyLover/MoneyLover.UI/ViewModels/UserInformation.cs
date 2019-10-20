using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class UserInformation : Validates.AccountValidate
    {
        public Views.UserInformation userInformation;
        private int UserID = Convert.ToInt32(Application.Current.Resources["current_user_id"]);
        public UserInformation()
        {
            userInformation = new Views.UserInformation();
            LoadWallet();
            DisableInformation();

            userInformation.btnEdit.Click += (sender, e) =>
            {
                userInformation.txtWallet.IsEnabled = userInformation.txtWallet.IsEnabled = true;
            };

            userInformation.btnSave.Click += (sender, e) =>
             {
                 try
                 {
                     double wallet = getNumber(userInformation.txtWallet.Text);
                     if (IsValidWallet(wallet))
                     {
                         using (var db = new DB.MoneyLoverDB())
                         {
                             Models.User user = db.Users.Find(UserID);
                             user.Wallet = wallet;
                             db.SaveChanges();
                         }
                         Close();
                     }
                 }
                 catch
                 {
                     MessageBox.Show("Đã có lỗi xảy ra, vui lòng kiểm tra lại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                 }
             };

            userInformation.btnClose.Click += (sender, e) =>
            {
                Close();
            };

            userInformation.btnCancel.Click += (sender, e) =>
            {
                Close();
            };
        }

        public void LoadWallet()
        {
            userInformation.txtEmail.Text =Models.User.GetUser(UserID).Email.ToString();
            userInformation.txtWallet.Text =  Models.User.GetUser(UserID).Wallet.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat) + " đ";
            userInformation.txtSavingWallet.Text = Models.User.GetUser(UserID).SavingsWallet.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat) + " đ";
        }

        public void DisableInformation()
        {
            userInformation.txtEmail.IsEnabled = userInformation.txtEmail.IsEnabled = false;
            userInformation.txtWallet.IsEnabled = userInformation.txtWallet.IsEnabled = false;
            userInformation.txtSavingWallet.IsEnabled = userInformation.txtSavingWallet.IsEnabled = false;

        }

        public void Show()
        {
            userInformation.Show();
        }
        public void Close()
        {
            userInformation.Close();
        }

        public void ShowDialog()
        {
            userInformation.ShowDialog();
        }
    }
    
    
}
