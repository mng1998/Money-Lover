using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.ViewModels
{
    public class PassbookList : Services.AccountService
    {
        public Views.PassbookList passBookList;
        private PassBook passBook;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();

        public PassbookList()
        {
            passBookList = new Views.PassbookList();

            passBookList.dtgridListPassBook.ItemsSource = db.PassBooks.Where(m => m.Settlement == false).ToList();

            passBookList.btnAddPassBook.Click += (sender, e) =>
            {
                passBook = new PassBook(this);
                passBook.ShowDialog();
            };
            

            passBookList.btnBack.Click += (sender, e) =>
            {
                passBookList.Close();
                Logout();
            };
        }

        public void Show()
        {
            passBookList.Show();
        }
    }
}
