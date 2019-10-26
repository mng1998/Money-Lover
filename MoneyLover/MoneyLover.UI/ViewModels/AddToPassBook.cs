using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class AddToPassBook : Validates.PassBookValidate
    {
        private Views.AddToPassBook addToPassBook;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();
        private Models.PassBook passBook;

        public AddToPassBook(PassbookList pbList, Models.PassBook pb)
        {
            addToPassBook = new Views.AddToPassBook();
            passBook = pb;

            placeData();

            addToPassBook.btnSave.Click += (sender, e) =>
            {
                try
                {
                    Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                    if (addToPassBook.txtAddMoreDeposit.Text != "")
                    {
                        double moneyAdd = getNumber(addToPassBook.txtAddMoreDeposit.Text);
                        if (ValidateAddDeposit(pb.UserID, moneyAdd, passBook))
                        {
                            Models.User user = db.Users.Find(pb.UserID);
                            user.Wallet -= moneyAdd;
                            user.SavingsWallet += moneyAdd;
                            passBook.Deposit += moneyAdd;

                            db.SaveChanges();
                            addToPassBook.Close();
                            placeData();
                            pbList.ShowDataGrid();
                        }
                    }
                    else
                        MessageBox.Show("Nội dung không được để rỗng", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng kiểm tra lại", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };

            addToPassBook.btnClose.Click += (sender, e) =>
            {
                addToPassBook.Close();
            };
        }

        public void placeData()
        {
            addToPassBook.txtBank.Text = Models.Bank.GetBank(passBook.BankID).BankName;
            addToPassBook.txtDeposit.Text = passBook.Deposit.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat);
            addToPassBook.txtInterestRates.Text = passBook.InterestRates.ToString("#\\%");
            addToPassBook.txtSentDate.Text = passBook.SentDate.ToString("dd/MM/yyyy");
            addToPassBook.txtEndDate.Text = passBook.EndDate.ToString("dd/MM/yyyy");
            addToPassBook.txtPassBookID.Text = "#" + passBook.GetID;
            addToPassBook.txtTerm.Text = passBook.Term.ToString("0 tháng");
        }

        public void ShowDialog()
        {
            addToPassBook.ShowDialog();
        }
    }
}
