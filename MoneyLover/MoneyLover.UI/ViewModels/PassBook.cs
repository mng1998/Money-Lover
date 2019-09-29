using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class PassBook
    {
        private Views.PassBook passBook;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();
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

        public PassBook()
        {
            passBook = new Views.PassBook();
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
                    db.PassBooks.Add(new Models.PassBook {
                        BankID = BankID,
                        Deposit = Convert.ToDouble(passBook.txtDeposit.Text),
                        Due = dueKey,
                        IndefiniteTerm = GetIndefiniteTerm(Convert.ToDouble(passBook.txtIndefiniteTerm.Text)),
                        Term = TermKey, PayInterest = payInterestKey, SentDate = DateTime.Parse(passBook.dpDate.Text),
                        UserID = current_user.UserID,
                        InterestRates = Convert.ToDouble(passBook.txtInterestRates.Text),
                        Settlement = false
                    });

                    db.SaveChanges();
                }
            };
        }

        public void Show()
        {
            passBook.Show();
        }

        private bool IsDateBeforeOrToday(string input)
        {
            DateTime pDate = DateTime.Parse(input);
            if (pDate <= DateTime.Now)
                return true;
            else
            {
                MessageBox.Show("Ngày gửi phải bé hơn hoặc bằng ngày hiện tại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private bool ValidateDeposit(double deposit)
        {
            if (deposit < 1000000)
            {
                MessageBox.Show("Số tiền gửi tối thiểu là 1.000.000đ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else return true;
        }

        private double GetIndefiniteTerm(double IndefiniteTerm)
        {
            if (IndefiniteTerm == 0)
                return 0.05;
            else
                return IndefiniteTerm;
        }
    }
}
