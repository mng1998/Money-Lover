using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyLover.UI.Models;

namespace MoneyLover.UI.Services
{
    public class AccountService
    {
        DB.MoneyLoverDB db = new DB.MoneyLoverDB();
        public bool Register(string email, string password)
        {
            if (IsValidEmail(email) && IsValidPassword(password))
            {
                User checkUser = db.Users.Where(m => m.Email == email).SingleOrDefault();
                if (checkUser == null)
                {
                    db.Users.Add(new User { Email = email, Password = password });
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
            User checkUser = db.Users.Where(m => m.Email == email).SingleOrDefault();
            if (checkUser != null && checkUser.Password == password) return true;
            else return false;
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

        private bool IsValidPassword(string password)
        {
            return
               password.Any(c => IsLetter(c)) &&
               password.Any(c => IsDigit(c)) &&
               password.Any(c => IsSymbol(c));
        }

        private bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetter(c);
        }
    }
}
