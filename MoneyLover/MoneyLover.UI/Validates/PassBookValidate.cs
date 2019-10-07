using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.Validates
{
    public class PassBookValidate
    {
        public bool IsDateBeforeOrToday(string input)
        {
            DateTime pDate = DateTime.Parse(input);
            if (pDate <= DateTime.Now)
                return true;
            else
            {
                MessageBox.Show("Ngày gửi phải bé hơn hoặc bằng ngày hiện tại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        public bool ValidateDeposit(int UserID, double deposit)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                if (deposit < 1000000 && deposit < Models.User.GetUser(UserID).Wallet)
                {
                    MessageBox.Show("Số tiền gửi tối thiểu là 1.000.000đ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else return true;
            }
        }

        public double ValidateAddDeposit(int UserID, double deposit)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                if (deposit < 100000 && deposit < Models.User.GetUser(UserID).Wallet)
                {
                    MessageBox.Show("Số tiền gửi thêm tối thiểu là 100.000đ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }
                else return deposit;
            }
        }

        public double GetIndefiniteTerm(string IndefiniteTerm)
        {
            if (IndefiniteTerm == "")
                return 0.05;
            else
                return Convert.ToDouble(IndefiniteTerm);
        }
    }
}
