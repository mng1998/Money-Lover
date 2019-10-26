using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            catch (ArgumentNullException)
            {
                MessageBox.Show("Email không được null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Không được để trống email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            catch (FormatException)
            {
                MessageBox.Show("Nhập sai định dạng email!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool IsValidPassword(string password)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasChars = new Regex(@"[a-zA-Z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");
            var hasSpecialChars = new Regex(@"[!@#$&*]+");

            if (!hasMinimum8Chars.IsMatch(password))
            {
                MessageBox.Show("Mật khẩu tối thiểu có 8 ký tự!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                if (hasNumber.IsMatch(password) && hasChars.IsMatch(password) && hasSpecialChars.IsMatch(password))
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
    }
}
