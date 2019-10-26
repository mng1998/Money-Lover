using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class EditPassBook : Validates.PassBookValidate
    {
        private Views.EditPassBook editPassBook;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();
        private Models.PassBook passBook;
        private Models.User current_user;

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

        public EditPassBook(PassbookList pbList, Models.PassBook pb)
        {
            editPassBook = new Views.EditPassBook();
            passBook = pb;
            current_user = db.Users.Find(Application.Current.Resources["current_user_id"]);

            editPassBook.cbbBank.ItemsSource = db.Banks.ToList();
            editPassBook.cbbBank.SelectedValuePath = "BankID";
            editPassBook.cbbBank.DisplayMemberPath = "BankName";

            editPassBook.cbbTerm.ItemsSource = term;
            editPassBook.cbbTerm.SelectedValuePath = "Keys";
            editPassBook.cbbTerm.DisplayMemberPath = "Value";

            editPassBook.cbbPayInterest.ItemsSource = payInterest;
            editPassBook.cbbPayInterest.SelectedValuePath = "Keys";
            editPassBook.cbbPayInterest.DisplayMemberPath = "Value";

            editPassBook.cbbDue.ItemsSource = due;
            editPassBook.cbbDue.SelectedValuePath = "Keys";
            editPassBook.cbbDue.DisplayMemberPath = "Value";

            placeData();
            editPassBook.btnSave.Click += (sender, e) =>
            {
                try
                {
                    Models.PassBook targetPb = db.PassBooks.Find(pb.PassBookID);
                    Models.User user = db.Users.Find(pb.UserID);

                    int BankID = Convert.ToInt32(editPassBook.cbbBank.SelectedValue);
                    int TermKey = Convert.ToInt32(((KeyValuePair<int, string>)editPassBook.cbbTerm.SelectedItem).Key);
                    int payInterestKey = Convert.ToInt32(((KeyValuePair<int, string>)editPassBook.cbbPayInterest.SelectedItem).Key);
                    int dueKey = Convert.ToInt32(((KeyValuePair<int, string>)editPassBook.cbbDue.SelectedItem).Key);
                    double moneyEdit = getNumber(editPassBook.txtDeposit.Text);

                    if (IsDateBeforeOrToday(editPassBook.dpDate.Text) && ValidateEditDeposit(pb.UserID, pb.Deposit, moneyEdit))
                    {
                        double backupDeposit = targetPb.Deposit;

                        targetPb.BankID = BankID;
                        targetPb.Due = dueKey;
                        targetPb.IndefiniteTerm = GetIndefiniteTerm(editPassBook.txtIndefiniteTerm.Text);
                        targetPb.Term = TermKey;
                        targetPb.PayInterest = payInterestKey;
                        targetPb.SentDate = DateTime.Parse(editPassBook.dpDate.Text);
                        targetPb.EndDate = DateTime.Parse(editPassBook.dpDate.Text).AddMonths(TermKey);
                        targetPb.UserID = current_user.UserID;
                        targetPb.InterestRates = Convert.ToDouble(editPassBook.txtInterestRates.Text);
                        targetPb.Settlement = false;
                        targetPb.Deposit = moneyEdit;

                        user.SavingsWallet = user.SavingsWallet - backupDeposit + moneyEdit;
                        user.Wallet = user.Wallet + backupDeposit - moneyEdit;

                        db.SaveChanges();
                        editPassBook.Close();
                        pbList.ShowDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng kiểm tra lại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            editPassBook.cbbTerm.SelectionChanged += (sender, e) =>
            {
                int TermKey = Convert.ToInt32(((KeyValuePair<int, string>)editPassBook.cbbTerm.SelectedItem).Key);
                if (TermKey == 99)
                {
                    editPassBook.txtIndefiniteTerm.IsEnabled = true;
                    editPassBook.txtInterestRates.Text = "0";
                    editPassBook.txtInterestRates.IsEnabled = false;
                }
                else
                {
                    editPassBook.txtInterestRates.IsEnabled = editPassBook.txtIndefiniteTerm.IsEnabled = true;
                }
            };

            editPassBook.btnCancel.Click += (sender, e) =>
            {
                editPassBook.Close();
            };

            editPassBook.btnClose.Click += (sender, e) =>
            {
                editPassBook.Close();
            };
        }

        public void placeData()
        {
            editPassBook.txtDeposit.Text = passBook.Deposit.ToString();
            editPassBook.txtIndefiniteTerm.Text = passBook.IndefiniteTerm.ToString();
            editPassBook.txtInterestRates.Text = passBook.InterestRates.ToString();
            editPassBook.dpDate.Text = passBook.SentDate.ToString();
            editPassBook.cbbBank.SelectedIndex = passBook.BankID - 1;
            editPassBook.cbbDue.SelectedIndex = passBook.Due - 1;
            editPassBook.cbbPayInterest.SelectedIndex = passBook.PayInterest - 1;
            editPassBook.cbbTerm.SelectedIndex = (passBook.Term == 99 ? 0 : (passBook.Term == 1 ? 1 : (passBook.Term == 3 ? 2 : (passBook.Term == 6 ? 3 : (passBook.Term == 12 ? 4 : 0)))));
            if (editPassBook.cbbTerm.SelectedIndex == 0)
            {
                editPassBook.txtIndefiniteTerm.IsEnabled = true;
                editPassBook.txtInterestRates.Text = "0";
                editPassBook.txtInterestRates.IsEnabled = false;
            }
            else
            {
                editPassBook.txtInterestRates.IsEnabled = editPassBook.txtIndefiniteTerm.IsEnabled = true;
            }
        }

        public void ShowDialog()
        {
            editPassBook.ShowDialog();
        }
    }
}
