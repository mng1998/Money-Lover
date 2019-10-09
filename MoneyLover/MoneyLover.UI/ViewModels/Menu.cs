using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.ViewModels
{
    public class Menu
    {
        private Views.Menu menu;

        public Menu(Models.PassBook pb)
        {
            menu = new Views.Menu();

            menu.txtBankName.Text = "Ngân hàng: " + Models.Bank.GetBank(pb.BankID).BankName;
            menu.txtDeposit.Text = "Số tiền gửi: " + pb.Deposit.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat) + " đ";
            menu.txtEndDate.Text = "Ngày đến hạn: " + pb.EndDate.ToString("dd/MM/yyyy");
            menu.txtPassBookID.Text = "Mã số: #" + pb.GetID;

            menu.btnClose.Click += (sender, e) =>
            {
                menu.Close();
            };
        }

        public void ShowDialog()
        {
            menu.ShowDialog();
        }
    }
}
