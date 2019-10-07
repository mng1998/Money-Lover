using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.Services
{
    public class WithDrawalService
    {
        private Dictionary<int, string> term = new Dictionary<int, string>
        {
            { 99, "Không thời hạn" },
            { 1, "1 tháng" },
            { 3, "3 tháng" },
            { 6, "6 tháng" },
            { 12, "12 tháng" },
        };

        private Dictionary<int, string> payInterest = new Dictionary<int, string>
        {
            { 1, "Cuối kỳ" },
            { 2, "Đầu kỳ" },
            { 3, "Định kỳ hằng tháng" }
        };

        private Dictionary<int, string> due = new Dictionary<int, string>
        {
            { 1, "Tái tục gốc và lãi" },
            { 2, "Tái tục gốc" },
            { 3, "Tất toán sổ" }
        };

        public static void DueChoice1(Models.PassBook pb, Models.User user)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                if (pb.PayInterest != 1)
                {
                    if (DateTime.Now >= pb.EndDate)
                    {
                        Models.User User = db.Users.Find(user.UserID);
                        Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                        if (pb.Term != 99)
                        {
                            passBook.Deposit += ((pb.Deposit * (pb.InterestRates / 100) * pb.Term) / 12);
                            User.SavingsWallet += ((pb.Deposit * (pb.InterestRates / 100) * pb.Term) / 12);
                            DateTime tmp = passBook.EndDate;
                            passBook.SentDate = tmp;
                            passBook.EndDate = tmp.AddMonths(passBook.Term);
                        }
                    }
                }
                else
                {
                    if ((DateTime.Now - pb.EndDate).Days == 1 || (DateTime.Now >= pb.EndDate))
                    {
                        Models.User User = db.Users.Find(user.UserID);
                        Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                        if (pb.Term != 99)
                        {
                            passBook.Deposit += ((pb.Deposit * (pb.InterestRates / 100) * pb.Term) / 12);
                            User.SavingsWallet += ((pb.Deposit * (pb.InterestRates / 100) * pb.Term) / 12);
                            DateTime tmp = passBook.EndDate;
                            passBook.SentDate = tmp;
                            passBook.EndDate = tmp.AddMonths(passBook.Term);
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public static void DueChoice2(Models.PassBook pb, Models.User user)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                if (pb.PayInterest != 1)
                {
                    if (DateTime.Now >= pb.EndDate)
                    {
                        Models.User User = db.Users.Find(user.UserID);
                        Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                        if (pb.Term != 99)
                        {
                            User.Wallet += ((pb.Deposit * (pb.InterestRates / 100) * pb.Term) / 12);
                            DateTime tmp = passBook.EndDate;
                            passBook.SentDate = tmp;
                            passBook.EndDate = tmp.AddMonths(passBook.Term);
                        }
                    }
                }
                else
                {
                    if ((DateTime.Now - pb.EndDate).Days == 1 || (DateTime.Now >= pb.EndDate))
                    {
                        Models.User User = db.Users.Find(user.UserID);
                        Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                        if (pb.Term != 99)
                        {
                            User.Wallet += ((pb.Deposit * (pb.InterestRates / 100) * pb.Term) / 12);
                            DateTime tmp = passBook.EndDate;
                            passBook.SentDate = tmp;
                            passBook.EndDate = tmp.AddMonths(passBook.Term);
                        }
                    }
                }
                db.SaveChanges();
            }
        }

        public static void DueChoice3(Models.PassBook pb, Models.User user)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                if (pb.PayInterest != 1)
                {
                    if (DateTime.Now >= pb.EndDate)
                    {
                        Models.User User = db.Users.Find(user.UserID);
                        Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                        if (pb.Term != 99)
                        {
                            User.Wallet += (pb.Deposit + ((pb.Deposit * (pb.InterestRates / 100) * pb.Term) / 12));
                            User.SavingsWallet -= pb.Deposit;
                            passBook.Settlement = true;
                        }
                    }
                }
                else
                {
                    if ((DateTime.Now - pb.EndDate).Days == 1 || (DateTime.Now >= pb.EndDate))
                    {
                        Models.User User = db.Users.Find(user.UserID);
                        Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                        if (pb.Term != 99)
                        {
                            User.Wallet += (pb.Deposit + ((pb.Deposit * (pb.InterestRates / 100) * pb.Term) / 12));
                            User.SavingsWallet -= pb.Deposit;
                            passBook.Settlement = true;
                        }
                    }
                }
                db.SaveChanges();
            }
        }
    }
}
