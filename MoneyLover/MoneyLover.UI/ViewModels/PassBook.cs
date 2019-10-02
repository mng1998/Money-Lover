using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class PassBook : Validates.PassBookValidate
    {
        public Views.PassBook passBook;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();
        private Bank bank;

        private Services.PassBookService pbService;

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

        public PassBook(PassbookList pblist)
        {
            passBook = new Views.PassBook();
            pbService = new Services.PassBookService();
            Models.User current_user = db.Users.Find(Application.Current.Resources["current_user_id"]);

            passBook.cbbBank.ItemsSource = db.Banks.ToList();
            passBook.cbbBank.SelectedValuePath = "BankID";
            passBook.cbbBank.DisplayMemberPath = "BankName";
            passBook.cbbBank.SelectedIndex = 0;

            passBook.cbbTerm.ItemsSource = term;
            passBook.cbbTerm.SelectedValuePath = "Keys";
            passBook.cbbTerm.DisplayMemberPath = "Value";
            passBook.cbbTerm.SelectedIndex = 0;

            passBook.cbbPayInterest.ItemsSource = payInterest;
            passBook.cbbPayInterest.SelectedValuePath = "Keys";
            passBook.cbbPayInterest.DisplayMemberPath = "Value";
            passBook.cbbPayInterest.SelectedIndex = 0;

            passBook.cbbDue.ItemsSource = due;
            passBook.cbbDue.SelectedValuePath = "Keys";
            passBook.cbbDue.DisplayMemberPath = "Value";
            passBook.cbbDue.SelectedIndex = 0;

            passBook.btnSave.Click += (sender, e) =>
            {
                int BankID = Convert.ToInt32(passBook.cbbBank.SelectedValue);
                int TermKey = Convert.ToInt32(((KeyValuePair<int, string>)passBook.cbbTerm.SelectedItem).Key);
                int payInterestKey = Convert.ToInt32(((KeyValuePair<int, string>)passBook.cbbPayInterest.SelectedItem).Key);
                int dueKey = Convert.ToInt32(((KeyValuePair<int, string>)passBook.cbbDue.SelectedItem).Key);

                if (IsDateBeforeOrToday(passBook.dpDate.Text) && ValidateDeposit(Convert.ToDouble(passBook.txtDeposit.Text)))
                {
                    pbService.Create(BankID, 
                                     Convert.ToDouble(passBook.txtDeposit.Text), 
                                     dueKey, 
                                     GetIndefiniteTerm(Convert.ToDouble(passBook.txtIndefiniteTerm.Text)), 
                                     TermKey, 
                                     payInterestKey, 
                                     DateTime.Parse(passBook.dpDate.Text), 
                                     current_user.UserID, 
                                     Convert.ToDouble(passBook.txtInterestRates.Text));
                }
            };

            passBook.btnAddBank.Click += (sender, e) =>
            {
                bank = new Bank(this);
                bank.ShowDialog();
            };

            passBook.btnCancel.Click += (sender, e) =>
            {
                passBook.Close();
            };

            passBook.btnClose.Click += (sender, e) =>
            {
                passBook.Close();
                pblist.passBookList.dtgridListPassBook.ItemsSource = db.PassBooks.Where(m => m.Settlement == false).ToList();
                pblist.passBookList.dtgridSettlement.ItemsSource = db.PassBooks.Where(m => m.Settlement == true).ToList();
            }; 
        }

        public void ShowDialog()
        {
            passBook.ShowDialog();
        }
    }
}
