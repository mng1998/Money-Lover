﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.Validates
{
    public class PassBookValidate : AccountValidate
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
                Models.User user = db.Users.Find(UserID);
                if (deposit < 1000000)
                {
                    MessageBox.Show("Số tiền gửi tối thiểu là 1.000.000đ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (deposit > user.Wallet)
                {
                    MessageBox.Show("Số tiền gửi phải nhỏ hơn hoặc bằng số tiền mặt hiện có", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else return true;
            }
        }

        public bool ValidateAddDeposit(int UserID, double deposit, Models.PassBook passBook)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                if (passBook.Due != 3)
                {
                    Models.User user = db.Users.Find(UserID);
                    if (deposit < 100000)
                    {
                        MessageBox.Show("Số tiền gửi thêm tối thiểu là 100.000đ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else if (deposit > user.Wallet)
                    {
                        MessageBox.Show("Số tiền gửi phải bé hơn tiền mặt hiện có", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else if ((DateTime.Now - passBook.EndDate).Days != 0)
                    {
                        MessageBox.Show("Chỉ được gửi thêm tiền khi đến kỳ hạn tính lãi suất", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                    else return true;
                }
                else
                {
                    MessageBox.Show("Gửi thêm tiền không áp dụng cho tất toán sổ khi đến hạn", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }
        }

        public bool ValidateEditDeposit(int UserID, double olddeposit, double deposit)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                Models.User user = db.Users.Find(UserID);
                if (deposit < 1000000)
                {
                    MessageBox.Show("Số tiền không được bé hơn 1.000.000đ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else if (deposit > (user.Wallet + olddeposit))
                {
                    MessageBox.Show("Số tiền gửi phải bé hơn tiền mặt hiện có", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                else return true;
            }
        }

        public double GetIndefiniteTerm(string IndefiniteTerm)
        {
            if (IndefiniteTerm == "")
                return 0.05;
            else return Convert.ToDouble(IndefiniteTerm);
        }
    }
}
