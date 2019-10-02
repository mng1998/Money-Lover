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

        public bool ValidateDeposit(double deposit)
        {
            if (deposit < 1000000)
            {
                MessageBox.Show("Số tiền gửi tối thiểu là 1.000.000đ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else return true;
        }

        public double GetIndefiniteTerm(double IndefiniteTerm)
        {
            if (IndefiniteTerm == 0)
                return 0.05;
            else
                return IndefiniteTerm;
        }
    }
}
