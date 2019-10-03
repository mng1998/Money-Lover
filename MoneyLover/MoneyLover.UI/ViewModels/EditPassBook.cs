﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class EditeditPassBook : Validates.PassBookValidate
    {
        private Views.EditPassBook editPassBook;
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

        public EditeditPassBook(PassbookList pblist, Models.PassBook pb)
        {
            editPassBook = new Views.EditPassBook();
            Models.User current_user = db.Users.Find(Application.Current.Resources["current_user_id"]);

            editPassBook.cbbBank.ItemsSource = db.Banks.ToList();
            editPassBook.cbbBank.SelectedValuePath = "BankID";
            editPassBook.cbbBank.DisplayMemberPath = "BankName";
            editPassBook.cbbBank.SelectedIndex = 0;

            editPassBook.cbbTerm.ItemsSource = term;
            editPassBook.cbbTerm.SelectedValuePath = "Keys";
            editPassBook.cbbTerm.DisplayMemberPath = "Value";
            editPassBook.cbbTerm.SelectedIndex = 0;

            editPassBook.cbbPayInterest.ItemsSource = payInterest;
            editPassBook.cbbPayInterest.SelectedValuePath = "Keys";
            editPassBook.cbbPayInterest.DisplayMemberPath = "Value";
            editPassBook.cbbPayInterest.SelectedIndex = 0;

            editPassBook.cbbDue.ItemsSource = due;
            editPassBook.cbbDue.SelectedValuePath = "Keys";
            editPassBook.cbbDue.DisplayMemberPath = "Value";
            editPassBook.cbbDue.SelectedIndex = 0;


            editPassBook.btnSave.Click += (sender, e) =>
            {
                int BankID = Convert.ToInt32(editPassBook.cbbBank.SelectedValue);
                int TermKey = Convert.ToInt32(((KeyValuePair<int, string>)editPassBook.cbbTerm.SelectedItem).Key);
                int payInterestKey = Convert.ToInt32(((KeyValuePair<int, string>)editPassBook.cbbPayInterest.SelectedItem).Key);
                int dueKey = Convert.ToInt32(((KeyValuePair<int, string>)editPassBook.cbbDue.SelectedItem).Key);

                if (IsDateBeforeOrToday(editPassBook.dpDate.Text) && ValidateDeposit(Convert.ToDouble(editPassBook.txtDeposit.Text)))
                {
                    pb.BankID = BankID;
                    pb.Deposit = Convert.ToDouble(editPassBook.txtDeposit.Text);
                    pb.Due = dueKey;
                    pb.IndefiniteTerm = GetIndefiniteTerm(Convert.ToDouble(editPassBook.txtIndefiniteTerm.Text));
                    pb.Term = TermKey;
                    pb.PayInterest = payInterestKey;
                    pb.SentDate = DateTime.Parse(editPassBook.dpDate.Text);
                    pb.UserID = current_user.UserID;
                    pb.InterestRates = Convert.ToDouble(editPassBook.txtInterestRates.Text);
                    pb.Settlement = false;

                    // xử lý tính toán

                    db.SaveChanges();
                }
            };

            editPassBook.btnCancel.Click += (sender, e) =>
            {
                editPassBook.Close();
                pblist.passBookList.dtgridListPassBook.ItemsSource = db.PassBooks.Where(m => m.Settlement == false).ToList();
                pblist.passBookList.dtgridSettlement.ItemsSource = db.PassBooks.Where(m => m.Settlement == true).ToList();
            };

            editPassBook.btnClose.Click += (sender, e) =>
            {
                editPassBook.Close();
                pblist.passBookList.dtgridListPassBook.ItemsSource = db.PassBooks.Where(m => m.Settlement == false).ToList();
                pblist.passBookList.dtgridSettlement.ItemsSource = db.PassBooks.Where(m => m.Settlement == true).ToList();
            };
        }

        public void ShowDialog()
        {
            editPassBook.ShowDialog();
        }
    }
}