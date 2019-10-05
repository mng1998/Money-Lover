using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
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

            ShowDataGrid(false);
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

        public void ShowPassBookList(string BankShortName, List<Models.PassBook> passBook, bool reload)
        {
            if (reload == false)
            {
                GroupBox groupBoxBank = new GroupBox();
                groupBoxBank.Height = 220;
                groupBoxBank.Style = Application.Current.FindResource("MaterialDesignGroupBox") as Style;
                groupBoxBank.Margin = new Thickness(16);
                ColorZoneAssist.SetMode(groupBoxBank, ColorZoneMode.PrimaryDark);
                groupBoxBank.Header = BankShortName;

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
                passBookList.RegisterName(BankShortName, dtgrid);
                passBookList.ListPassBook.Children.Add(groupBoxBank);
            }
            else
            {
                DataGrid dtgrid = (DataGrid)passBookList.FindName(BankShortName);
                if (passBook.Count != 0)
                    dtgrid.ItemsSource = passBook;
                else
                {
                    passBookList.ListPassBook.Children.Remove(dtgrid);
                    passBookList.UnregisterName(BankShortName);
                    dtgrid = null;
                }

            }
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

        public void ShowDataGrid(bool reload)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                foreach (int BankID in GetListBankOfUser())
                {
                    var ListPassBookOfBank = db.PassBooks.Where(m => m.Settlement == false && m.UserID == UserID && m.BankID == BankID).ToList();
                    ShowPassBookList(Models.Bank.GetBankName(BankID), ListPassBookOfBank, reload);
                }
            }
        }
    }
}
