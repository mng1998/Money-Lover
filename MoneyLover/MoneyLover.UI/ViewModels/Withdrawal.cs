using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class Withdrawal : Validates.AccountValidate
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
                try
                {
                    Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                    Models.User targetUser = db.Users.Find(passBook.UserID);

                    targetUser.Wallet += getNumber(withDrawal.txtTotalMoney.Text);
                    targetUser.SavingsWallet -= pb.Deposit;
                    passBook.Settlement = true;

                    db.SaveChanges();

                    withDrawal.Close();
                    pbList.ShowDataGrid();
                    pbList.passBookList.dtgridSettlement.ItemsSource = Models.PassBook.getListPassBookSettlement(UserID);
                    pbList.LoadHeaderSettlement();
                }
                catch
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng kiểm tra lại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            withDrawal.btnClose.Click += (sender, e) =>
            {
                withDrawal.Close();
            };
        }

        public void placeData()
        {
            withDrawal.txtPassBookID.Text = "#" + passBook.GetID;
            withDrawal.txtBank.Text = Models.Bank.GetBank(passBook.BankID).BankName;
            withDrawal.txtSentDate.Text = passBook.SentDate.ToString("dd/MM/yyyy");
            withDrawal.txtTerm.Text = passBook.Term.ToString("0 tháng");
            withDrawal.txtInterestRates.Text = passBook.InterestRates.ToString("#\\%");
            withDrawal.txtDeposit.Text = passBook.Deposit.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat);
            withDrawal.txtEndDate.Text = passBook.EndDate.ToString("dd/MM/yyyy");

            if (passBook.EndDate <= DateTime.Now)
                withDrawal.txtTotalMoney.Text = (passBook.Deposit + (passBook.Deposit * (passBook.InterestRates / 100) * passBook.Term) / 12).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat);
            else
                withDrawal.txtTotalMoney.Text = (passBook.Deposit + (passBook.Deposit * (passBook.IndefiniteTerm / 100) * passBook.Term) / 12).ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat);
        }

        public void ShowDialog()
        {
            withDrawal.ShowDialog();
        }
    }
}
