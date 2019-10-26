using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MoneyLover.UI.ViewModels
{
    public class PassBook : Validates.PassBookValidate
    {
        public Views.PassBook passBook;
        private Bank bank;
        private Models.User current_user;

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

        public PassBook(PassbookList pbList)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                passBook = new Views.PassBook();
                pbService = new Services.PassBookService();
                current_user = db.Users.Find(Application.Current.Resources["current_user_id"]);

                passBook.cbbBank.ItemsSource = db.Banks.ToList();
                passBook.cbbBank.SelectedValuePath = "BankID";
                passBook.cbbBank.DisplayMemberPath = "BankName";
                passBook.cbbBank.SelectedIndex = 0;

                passBook.cbbTerm.ItemsSource = term;
                passBook.cbbTerm.SelectedValuePath = "Keys";
                passBook.cbbTerm.DisplayMemberPath = "Value";
                passBook.cbbTerm.SelectedIndex = 1;

                passBook.cbbPayInterest.ItemsSource = payInterest;
                passBook.cbbPayInterest.SelectedValuePath = "Keys";
                passBook.cbbPayInterest.DisplayMemberPath = "Value";
                passBook.cbbPayInterest.SelectedIndex = 0;

                passBook.cbbDue.ItemsSource = due;
                passBook.cbbDue.SelectedValuePath = "Keys";
                passBook.cbbDue.DisplayMemberPath = "Value";
                passBook.cbbDue.SelectedIndex = 0;
            }

            passBook.btnSave.Click += (sender, e) =>
            {
                try
                {
                    int BankID = Convert.ToInt32(passBook.cbbBank.SelectedValue);
                    int TermKey = Convert.ToInt32(((KeyValuePair<int, string>)passBook.cbbTerm.SelectedItem).Key);
                    int payInterestKey = Convert.ToInt32(((KeyValuePair<int, string>)passBook.cbbPayInterest.SelectedItem).Key);
                    int dueKey = Convert.ToInt32(((KeyValuePair<int, string>)passBook.cbbDue.SelectedItem).Key);
                    Models.Bank Bank = Models.Bank.GetBank(BankID);

                    if (IsDateBeforeOrToday(passBook.dpDate.Text) && ValidateDeposit(current_user.UserID, Convert.ToDouble(passBook.txtDeposit.Text)))
                    {
                        Models.PassBook pb = pbService.Create(BankID,
                                         Convert.ToDouble(passBook.txtDeposit.Text),
                                         dueKey,
                                         GetIndefiniteTerm(passBook.txtIndefiniteTerm.Text),
                                         TermKey,
                                         payInterestKey,
                                         DateTime.Parse(passBook.dpDate.Text),
                                         current_user.UserID,
                                         Convert.ToDouble(passBook.txtInterestRates.Text));

                        pbList.ShowPassBookList(Bank, Models.PassBook.getListPassBook(current_user.UserID, BankID));
                        passBook.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng kiểm tra lại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            passBook.btnAddBank.Click += (sender, e) =>
            {
                bank = new Bank(this);
                bank.ShowDialog();
            };

            passBook.cbbTerm.SelectionChanged += (sender, e) =>
            {
                int TermKey = Convert.ToInt32(((KeyValuePair<int, string>)passBook.cbbTerm.SelectedItem).Key);
                if (TermKey == 99)
                {
                    passBook.txtIndefiniteTerm.IsEnabled = true;
                    passBook.txtInterestRates.Text = "0";
                    passBook.txtInterestRates.IsEnabled = false;
                }
                else
                {
                    passBook.txtInterestRates.IsEnabled = passBook.txtIndefiniteTerm.IsEnabled = true;
                }
            };

            passBook.btnCancel.Click += (sender, e) =>
            {
                passBook.Close();
            };

            passBook.btnClose.Click += (sender, e) =>
            {
                passBook.Close();
            };
        }

        public void ShowDialog()
        {
            passBook.ShowDialog();
        }
    }
}
