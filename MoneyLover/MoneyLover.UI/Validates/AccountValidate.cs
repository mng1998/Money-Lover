using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.Validates
{
    public class AccountValidate
    {
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                MessageBox.Show("Nhập sai định dạng email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            if (password.Length < 8)
            {
                MessageBox.Show("Mật khẩu tối thiểu có 8 ký tự!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                if (password.Any(c => IsLetter(c)) && password.Any(c => IsDigit(c)) && password.Any(c => IsSymbol(c)))
                    return true;
                else
                {
                    MessageBox.Show("Nhập sai định dạng Mật khẩu!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        public double getNumber(string value)
        {
            var allowedChars = "01234567890";
            string result = new string(value.Where(c => allowedChars.Contains(c)).ToArray());
            return Convert.ToDouble(result);
        }

        public bool IsValidWallet(double wallet)
        {
            return (wallet > 0);
        }

        public bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        public bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        public bool IsSymbol(char c)
        {
            return c > 32 && c < 127 && !IsDigit(c) && !IsLetter(c);
        }
    }
}
