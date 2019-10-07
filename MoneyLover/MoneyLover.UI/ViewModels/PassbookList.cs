using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace MoneyLover.UI.ViewModels
{
    public class PassbookList : Services.AccountService
    {
        public Views.PassbookList passBookList;
        private PassBook passBook;
        private int UserID = Convert.ToInt32(Application.Current.Resources["current_user_id"]);
        private Models.PassBook lastSelectedItem;

        public PassbookList()
        {
            passBookList = new Views.PassbookList();

            LoadWithDrawal();
            ShowDataGrid();
            LoadHeaderSettlement();

            passBookList.dtgridSettlement.ItemsSource = Models.PassBook.getListPassBookSettlement(UserID);

            passBookList.btnAddPassBook.Click += (sender, e) =>
            {
                passBook = new PassBook(this);
                passBook.ShowDialog();
            };

            passBookList.btnEditPassBook.Click += (sender, e) =>
            {
                try
                {
                    EditPassBook editPassBook = new EditPassBook(this, lastSelectedItem);
                    editPassBook.ShowDialog();
                }
                catch { }
            };

            passBookList.btnAddMore.Click += (sender, e) =>
            {
                try
                {
                    AddToPassBook addMore = new AddToPassBook(this, lastSelectedItem);
                    addMore.ShowDialog();
                }
                catch { }
            };

            passBookList.btnSettlement.Click += (sender, e) =>
            {
                try
                {
                    Withdrawal withDrawal = new Withdrawal(this, lastSelectedItem);
                    withDrawal.ShowDialog();
                }
                catch { }
            };

            passBookList.btnWithDrawal.Click += (sender, e) =>
            {
                try
                {
                    PartialWithdrawal partialWithDrawal = new PartialWithdrawal(this, lastSelectedItem);
                    partialWithDrawal.ShowDialog();
                }
                catch { }
            };

            passBookList.btnViewPassBook.Click += (sender, e) =>
            {
                try
                {
                    Models.PassBook pb = (Models.PassBook)passBookList.dtgridSettlement.SelectedItem;
                    Menu menu = new Menu(pb);
                    menu.ShowDialog();
                }
                catch { }
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

        public void ShowPassBookList(Models.Bank Bank, List<Models.PassBook> passBook)
        {
            GroupBox groupBox = (GroupBox)passBookList.FindName(Bank.ShortName + Bank.BankID);

            if (groupBox == null && passBook.Count != 0)
            {
                GroupBox groupBoxBank = new GroupBox();
                groupBoxBank.Height = 220;
                groupBoxBank.Style = Application.Current.FindResource("MaterialDesignGroupBox") as Style;
                groupBoxBank.Margin = new Thickness(16);
                ColorZoneAssist.SetMode(groupBoxBank, ColorZoneMode.PrimaryDark);
                groupBoxBank.Header = Bank.ShortName + " (" + Models.PassBook.TotalMoneyPassBookOfBank(UserID, Bank.BankID, false) + " đ)";

                Grid grid = new Grid();
                DataGrid dtgrid = new DataGrid();
                dtgrid.AutoGenerateColumns = false;
                dtgrid.IsReadOnly = true;

                dtgrid.Columns.Add(SetDataGridTextColumn("Mã số", "GetID"));
                dtgrid.Columns.Add(SetDataGridTextColumn("Tổng số tiền gốc", "Deposit"));
                dtgrid.Columns.Add(SetDataGridTextColumn("Kỳ hạn gửi", "Term"));
                dtgrid.Columns.Add(SetDataGridTextColumn("Lãi suất năm", "InterestRates"));
                dtgrid.Columns.Add(SetDataGridTextColumn("Ngày mở", "SentDate"));

                dtgrid.SelectionChanged += Dtgrid_SelectionChanged;
                dtgrid.ItemsSource = passBook;
                grid.Children.Add(dtgrid);
                groupBoxBank.Content = grid;
                passBookList.RegisterName(Bank.ShortName, dtgrid);
                passBookList.RegisterName(Bank.ShortName + Bank.BankID, groupBoxBank);
                passBookList.ListPassBook.Children.Add(groupBoxBank);
            }
            else
            {
                DataGrid dtgrid = (DataGrid)passBookList.FindName(Bank.ShortName);
                if (passBook.Count != 0)
                {
                    dtgrid.ItemsSource = passBook;
                    groupBox.Header = Bank.ShortName + " (" + Models.PassBook.TotalMoneyPassBookOfBank(UserID, Bank.BankID, false) + " đ)";
                    LoadTextBlockPassBook();
                }
                else
                    RemoveGroupBoxBlank();
            }

            LoadWallet();
        }

        private void Dtgrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dtgrid = (DataGrid)sender;
            lastSelectedItem = (Models.PassBook)dtgrid.SelectedItem;
        }

        public DataGridTextColumn SetDataGridTextColumn(string header, string binding)
        {
            DataGridTextColumn dtgtextcolumn = new DataGridTextColumn();
            dtgtextcolumn.Header = header;
            dtgtextcolumn.Binding = new Binding(binding);
            dtgtextcolumn.IsReadOnly = true;

            if (header == "Ngày mở")
                dtgtextcolumn.Binding.StringFormat = "dd/MM/yyyy";

            if (header == "Tổng số tiền gốc")
                dtgtextcolumn.Binding.StringFormat = "#,###";

            return dtgtextcolumn;
        }

        public List<int> GetListBankOfUser()
        {
            using (var db = new DB.MoneyLoverDB())
            {
                var result = db.PassBooks.Where(m => m.UserID == UserID).Select(m => m.BankID).Distinct().ToList();
                return result;
            }
        }

        public void ShowDataGrid()
        {
            using (var db = new DB.MoneyLoverDB())
            {
                foreach (int BankID in GetListBankOfUser())
                {
                    var ListPassBookOfBank = db.PassBooks.Where(m => m.Settlement == false && m.UserID == UserID && m.BankID == BankID).ToList();
                    ShowPassBookList(Models.Bank.GetBank(BankID), ListPassBookOfBank);
                }

                LoadWallet();
                LoadTextBlockPassBook();
            }
        }

        public void LoadWallet()
        {
            passBookList.txtWallet.Text = "Ví tiền tiết kiệm: " + Models.User.GetUser(UserID).SavingsWallet.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat);
            passBookList.txtSavingsWallet.Text = "Ví tiền mặt: " + Models.User.GetUser(UserID).Wallet.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat);
        }

        public void LoadHeaderSettlement()
        {
            passBookList.groupBoxSettlement.Header = "Đã tất toán (" + Models.PassBook.CountPassBook(UserID, true).ToString() + " sổ)";
        }

        public void LoadTextBlockPassBook()
        {
            passBookList.txtTotalMoneyPassBook.Text = "Tổng tiền: " + Models.PassBook.TotalMoneyPassBook(UserID, false).ToString() + " (" + Models.PassBook.CountPassBook(UserID, false).ToString() + " sổ)";
        }

        public void RemoveGroupBoxBlank()
        {
            foreach (int BankID in GetListBankOfUser())
            {
                Models.Bank bank = Models.Bank.GetBank(BankID);
                GroupBox groupBox = (GroupBox)passBookList.FindName(bank.ShortName + bank.BankID);

                List<Models.PassBook> pbs = Models.PassBook.getListPassBook(UserID, BankID);

                if (pbs.Count == 0)
                {
                    try
                    {
                        passBookList.UnregisterName(bank.ShortName + bank.BankID);
                        passBookList.UnregisterName(bank.ShortName);
                        passBookList.ListPassBook.Children.Remove(groupBox);
                    }
                    catch { }
                }
            }
        }

        public void LoadWithDrawal()
        {
            foreach (var pb in Models.PassBook.getPassBooks(UserID))
            {
                switch(pb.Due)
                {
                    case 1:
                        Services.WithDrawalService.DueChoice1(pb, Models.User.GetUser(UserID));
                        break;
                    case 2:
                        Services.WithDrawalService.DueChoice2(pb, Models.User.GetUser(UserID));
                        break;
                    case 3:
                        Services.WithDrawalService.DueChoice3(pb, Models.User.GetUser(UserID));
                        break;
                }
            }
        }
    }
}
