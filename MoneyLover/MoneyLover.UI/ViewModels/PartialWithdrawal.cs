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
        private Models.PassBook passBook;

        public PartialWithdrawal(PassbookList pbList, Models.PassBook pb)
        {
            partialWithdrawal = new Views.PartialWithdrawal();
            passBook = pb;

            placeData();

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
                pbList.ShowDataGrid();
            };
        }

        public void placeData()
        {
            partialWithdrawal.txtPassBookID.Text = "#" + passBook.PassBookID.ToString();
            partialWithdrawal.txtSentDate.Text = passBook.SentDate.ToString();
            partialWithdrawal.txtTerm.Text = passBook.Term.ToString();
            partialWithdrawal.txtInterestRates.Text = passBook.InterestRates.ToString();
            partialWithdrawal.txtDeposit.Text = passBook.Deposit.ToString();
            partialWithdrawal.txtEndDate.Text = passBook.EndDate.ToString();
        }

        public void ShowDialog()
        {
            partialWithdrawal.ShowDialog();
        }
    }
}
