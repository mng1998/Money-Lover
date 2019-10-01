using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.ViewModels
{
    public class PartialWithdrawal
    {
        private Views.PartialWithdrawal partialWithdrawal;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();

        public PartialWithdrawal(PassbookList pbList, Models.PassBook pb)
        {
            partialWithdrawal = new Views.PartialWithdrawal();

            partialWithdrawal.txtPassBookID.Text += pb.PassBookID.ToString();
            partialWithdrawal.txtSentDate.Text = pb.SentDate.ToString();
            partialWithdrawal.txtTerm.Text = pb.Term.ToString();
            partialWithdrawal.txtInterestRates.Text = pb.InterestRates.ToString();
            partialWithdrawal.txtDeposit.Text = pb.Deposit.ToString();

            partialWithdrawal.btnSave.Click += (sender, e) =>
            {
                Models.PassBook passBook = db.PassBooks.Where(m => m.PassBookID == pb.PassBookID).FirstOrDefault();
                passBook.Deposit -= Convert.ToDouble(partialWithdrawal.txtWithDrawDeposit.Text);

                // Xử lý tính toán ở đây

                db.SaveChanges();
            };

            partialWithdrawal.btnClose.Click += (sender, e) =>
            {
                partialWithdrawal.Close();
                pbList.passBookList.dtgridListPassBook.ItemsSource = db.PassBooks.Where(m => m.Settlement == false).ToList();
                pbList.passBookList.dtgridSettlement.ItemsSource = db.PassBooks.Where(m => m.Settlement == true).ToList();
            };
        }

        public void ShowDialog()
        {
            partialWithdrawal.ShowDialog();
        }
    }
}
