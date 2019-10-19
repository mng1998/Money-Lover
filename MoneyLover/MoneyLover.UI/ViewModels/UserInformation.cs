using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class UserInformation : Services.AccountService
    {
        public Views.UserInformation userInformation;
        private int UserID = Convert.ToInt32(Application.Current.Resources["current_user_id"]);
        public UserInformation()
        {
            userInformation = new Views.UserInformation();
            LoadWallet();
            DisableInformation();
        }

        public void LoadWallet()
        {
            userInformation.txtUserId.Text = Models.User.GetUser(UserID).UserID.ToString();
            userInformation.txtEmail.Text =Models.User.GetUser(UserID).Email.ToString();
            userInformation.txtWallet.Text =  Models.User.GetUser(UserID).SavingsWallet.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat) + " đ";
            userInformation.txtSavingWallet.Text = Models.User.GetUser(UserID).Wallet.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat) + " đ";
        }

        public void DisableInformation()
        {
            userInformation.txtUserId.IsEnabled = userInformation.txtUserId.IsEnabled = false;
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
