using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class Withdrawal
    {
        private Views.Withdrawal withDrawal;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();
        private Models.PassBook passBook;
        private int UserID = Convert.ToInt32(Application.Current.Resources["current_user_id"]);

        public Withdrawal(PassbookList pbList, Models.PassBook pb)
        {
            withDrawal = new Views.Withdrawal();
            passBook = pb;

            placeData();

            withDrawal.btnSettlement.Click += (sender, e) =>
            {
                Models.PassBook passBook = db.PassBooks.Where(m => m.PassBookID == pb.PassBookID).FirstOrDefault();
                Models.User targetUser = db.Users.Find(passBook.UserID);
                targetUser.Wallet += Convert.ToDouble(withDrawal.txtTotalMoney.Text);
                passBook.Settlement = true;

                db.SaveChanges();

                withDrawal.Close();
                pbList.ShowDataGrid();
                pbList.passBookList.dtgridSettlement.ItemsSource = Models.PassBook.getListPassBookSettlement(UserID);
                pbList.LoadHeaderSettlement();
            };

            withDrawal.btnClose.Click += (sender, e) =>
            {
                withDrawal.Close();
            };
        }

        public void placeData()
        {
            withDrawal.txtPassBookID.Text = "#" + passBook.PassBookID.ToString();
            withDrawal.txtSentDate.Text = passBook.SentDate.ToString();
            withDrawal.txtTerm.Text = passBook.Term.ToString();
            withDrawal.txtInterestRates.Text = passBook.InterestRates.ToString();
            withDrawal.txtDeposit.Text = passBook.Deposit.ToString();
            withDrawal.txtEndDate.Text = passBook.EndDate.ToString();

            if (passBook.EndDate <= DateTime.Now)
                withDrawal.txtTotalMoney.Text = (passBook.Deposit + (passBook.Deposit * (passBook.InterestRates / 100) * passBook.Term) / 12).ToString();
            else
                withDrawal.txtTotalMoney.Text = (passBook.Deposit + (passBook.Deposit * (passBook.IndefiniteTerm / 100) * passBook.Term) / 12).ToString();
        }

        public void ShowDialog()
        {
            withDrawal.ShowDialog();
        }
    }
}
