using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.Services
{
    public class PassBookService
    {
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();
        public Models.PassBook Create(int bankID, double deposit, int due, double indefiniteTerm, int term, int payInterest, DateTime sentDate, int userID, double interestRates)
        {
            Models.PassBook pb = new Models.PassBook
            {
                BankID = bankID,
                Deposit = deposit,
                Due = due,
                IndefiniteTerm = indefiniteTerm,
                Term = term,
                PayInterest = payInterest,
                SentDate = sentDate,
                EndDate = getEndDate(sentDate, term),
                UserID = userID,
                InterestRates = interestRates,
                WithDrawalMoney = getWithDrawalMoney(deposit, interestRates, term),
                Settlement = false
            };

            db.PassBooks.Add(pb);
            db.SaveChanges();

            return pb;
        }

        public DateTime getEndDate(DateTime SentDate, int Term)
        {
            return SentDate.AddMonths(Term);
        }

        public double getWithDrawalMoney(double deposit, double interestRates, int term)
        {
            return deposit + (deposit * ((interestRates / 12) * term));
        }
    }
}
