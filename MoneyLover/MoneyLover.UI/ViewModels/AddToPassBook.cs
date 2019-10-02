using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.ViewModels
{
    public class AddToPassBook
    {
        private Views.AddToPassBook addToPassBook;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();

        public AddToPassBook(PassbookList pbList, Models.PassBook pb)
        {
            addToPassBook = new Views.AddToPassBook();

            addToPassBook.txtPassBookID.Text += pb.PassBookID.ToString();
            addToPassBook.txtSentDate.Text = pb.SentDate.ToString();
            addToPassBook.txtTerm.Text = pb.Term.ToString();
            addToPassBook.txtInterestRates.Text = pb.InterestRates.ToString();
            addToPassBook.txtDeposit.Text = pb.Deposit.ToString();

            addToPassBook.btnSave.Click += (sender, e) =>
            {
                Models.PassBook passBook = db.PassBooks.Where(m => m.PassBookID == pb.PassBookID).FirstOrDefault();
                passBook.Deposit += Convert.ToDouble(addToPassBook.txtAddMoreDeposit.Text);

                // Xử lý tính toán ở đây

                db.SaveChanges();
            };

            addToPassBook.btnClose.Click += (sender, e) =>
            {
                addToPassBook.Close();
                pbList.passBookList.dtgridListPassBook.ItemsSource = db.PassBooks.Where(m => m.Settlement == false).ToList();
                pbList.passBookList.dtgridSettlement.ItemsSource = db.PassBooks.Where(m => m.Settlement == true).ToList();
            };
        }

        public void ShowDialog()
        {
            addToPassBook.ShowDialog();
        }
    }
}
