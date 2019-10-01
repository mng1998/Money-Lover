using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.ViewModels
{
    public class Withdrawal
    {
        private Views.Withdrawal withDrawal;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();

        public Withdrawal(PassbookList pbList, Models.PassBook pb)
        {
            withDrawal = new Views.Withdrawal();

            withDrawal.txtPassBookID.Text += pb.PassBookID.ToString();
            withDrawal.txtSentDate.Text = pb.SentDate.ToString();
            withDrawal.txtTerm.Text = pb.Term.ToString();
            withDrawal.txtInterestRates.Text = pb.InterestRates.ToString();
            withDrawal.txtDeposit.Text = pb.Deposit.ToString();

            withDrawal.btnSettlement.Click += (sender, e) =>
            {
                Models.PassBook passBook = db.PassBooks.Where(m => m.PassBookID == pb.PassBookID).FirstOrDefault();
                passBook.Settlement = true;

                db.SaveChanges();
            };

            withDrawal.btnClose.Click += (sender, e) =>
            {
                withDrawal.Close();
                pbList.passBookList.dtgridListPassBook.ItemsSource = db.PassBooks.Where(m => m.Settlement == false).ToList();
                pbList.passBookList.dtgridSettlement.ItemsSource = db.PassBooks.Where(m => m.Settlement == true).ToList();
            };
        }

        public void ShowDialog()
        {
            withDrawal.ShowDialog();
        }
    }
}
