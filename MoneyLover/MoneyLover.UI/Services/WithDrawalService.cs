using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.Services
{
    public class WithDrawalService
    {
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
