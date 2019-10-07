﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.ViewModels
{
    public class AddToPassBook
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
                Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                passBook.Deposit += Convert.ToDouble(addToPassBook.txtAddMoreDeposit.Text);

                // Xử lý tính toán ở đây

                db.SaveChanges();
            };

            addToPassBook.btnClose.Click += (sender, e) =>
            {
                addToPassBook.Close();
                pbList.ShowDataGrid();
            };
        }

        public void placeData()
        {
            addToPassBook.txtDeposit.Text = passBook.Deposit.ToString();
            addToPassBook.txtInterestRates.Text = passBook.InterestRates.ToString();
            addToPassBook.txtSentDate.Text = passBook.SentDate.ToString("dd/MM/yyyy");
            addToPassBook.txtEndDate.Text = passBook.EndDate.ToString("dd/MM/yyyy");
            addToPassBook.txtPassBookID.Text = "#" + passBook.GetID;
            addToPassBook.txtTerm.Text = passBook.Term.ToString();
        }

        public void ShowDialog()
        {
            addToPassBook.ShowDialog();
        }
    }
}
