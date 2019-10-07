using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using MoneyLover.UI.Models;

namespace MoneyLover.UI.Services
{
    public class AccountService : Validates.AccountValidate
    {
        DB.MoneyLoverDB db = new DB.MoneyLoverDB();
        public bool Register(string email, string password)
        {
            if (IsValidEmail(email) && IsValidPassword(password))
            {
                User checkUser = db.Users.Where(m => m.Email == email).SingleOrDefault();
                if (checkUser == null)
                {
                    db.Users.Add(new User { Email = email, Password = password, Wallet = 100000000, SavingsWallet = 0 });
                    db.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            else return false;
        }

        public bool Login(string email, string password)
        {
            User checkUser = db.Users.Where(m => m.Email == email).FirstOrDefault();
            if (checkUser != null && checkUser.Password == password)
            {
                StoreUser(checkUser);
                return true;
            }
            else return false;
        }

        public void Logout()
        {
            Application.Current.Resources["current_user_id"] = "";
            Application.Current.Resources["user_signed_in"] = false;
        }

        private void StoreUser(User user)
        {
            Application.Current.Resources["current_user_id"] = user.UserID;
            Application.Current.Resources["user_signed_in"] = true;
        }
    }
}
