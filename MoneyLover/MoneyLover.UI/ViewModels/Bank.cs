using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.ViewModels
{
    public class Bank
    {
        private Views.Bank bank;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();

        public Bank(PassBook pb)
        {
            bank = new Views.Bank();
            bank.dtgridListBank.ItemsSource = db.Banks.ToList();

            bank.btnSave.Click += (sender, e) =>
            {
                db.Banks.Add(new Models.Bank { BankName = bank.txtBankName.Text, ShortName = bank.txtShortName.Text });
                db.SaveChanges();
                bank.dtgridListBank.ItemsSource = db.Banks.ToList();
                pb.passBook.cbbBank.ItemsSource = db.Banks.ToList();
            };

            bank.btnCancel.Click += (sender, e) =>
            {
                bank.Close();
            };

            bank.btnClose.Click += (sender, e) =>
            {
                bank.Close();
            };
        }

        public void ShowDialog()
        {
            bank.ShowDialog();
        }
    }
}
