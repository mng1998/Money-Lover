﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MoneyLover.UI.ViewModels
{
    public class PartialWithdrawal
    {
        private Views.PartialWithdrawal partialWithdrawal;
        private DB.MoneyLoverDB db = new DB.MoneyLoverDB();
        private Models.PassBook passBook;
        private int UserID = Convert.ToInt32(Application.Current.Resources["current_user_id"]);

        public PartialWithdrawal(PassbookList pbList, Models.PassBook pb)
        {
            partialWithdrawal = new Views.PartialWithdrawal();
            passBook = pb;

            placeData();

            partialWithdrawal.btnSave.Click += (sender, e) =>
            {
                Models.PassBook passBook = db.PassBooks.Find(pb.PassBookID);
                Models.User user = db.Users.Find(pb.UserID);

                if (pb.Term == 99)
                {
                    int day = Convert.ToInt32((DateTime.Now - pb.SentDate).TotalDays);
                    if (day > 15)
                    {
                        double moneyWithdrawal = Convert.ToDouble(partialWithdrawal.txtWithDrawDeposit.Text);

                        if (moneyWithdrawal < pb.Deposit)
                        {
                            user.Wallet += (moneyWithdrawal + (moneyWithdrawal * (passBook.IndefiniteTerm / 100) * day) / 365);
                            user.SavingsWallet -= moneyWithdrawal;
                            passBook.Deposit -= moneyWithdrawal;
                        }
                        else if( moneyWithdrawal == pb.Deposit)
                        {
                            user.Wallet += (moneyWithdrawal + (moneyWithdrawal * (passBook.IndefiniteTerm / 100) * day) / 365);
                            user.SavingsWallet -= moneyWithdrawal;
                            passBook.Settlement = true;
                        }
                    }
                }
                else
                {
                    if (DateTime.Now < pb.EndDate)
                    {
                        int day = Convert.ToInt32((DateTime.Now - pb.SentDate).TotalDays);
                        double moneyWithdrawal = Convert.ToDouble(partialWithdrawal.txtWithDrawDeposit.Text);

                        if (moneyWithdrawal < pb.Deposit)
                        {
                            user.Wallet += (moneyWithdrawal + (moneyWithdrawal * (passBook.IndefiniteTerm / 100) * day) / 365);
                            user.SavingsWallet -= moneyWithdrawal;
                            passBook.Deposit -= moneyWithdrawal;
                        }
                        else if (moneyWithdrawal == pb.Deposit)
                        {
                            user.Wallet += (moneyWithdrawal + (moneyWithdrawal * (passBook.IndefiniteTerm / 100) * day) / 365);
                            user.SavingsWallet -= moneyWithdrawal;
                            passBook.Settlement = true;
                        }
                    }
                }
                db.SaveChanges();

                partialWithdrawal.Close();
                pbList.ShowDataGrid();
                pbList.passBookList.dtgridSettlement.ItemsSource = Models.PassBook.getListPassBookSettlement(UserID);
                pbList.LoadHeaderSettlement();
            };

            partialWithdrawal.btnClose.Click += (sender, e) =>
            {
                partialWithdrawal.Close();
            };
        }

        public void placeData()
        {
            partialWithdrawal.txtPassBookID.Text = "#" + passBook.GetID;
            partialWithdrawal.txtBank.Text = Models.Bank.GetBank(passBook.BankID).BankName;
            partialWithdrawal.txtSentDate.Text = passBook.SentDate.ToString("dd/MM/yyyy");
            partialWithdrawal.txtTerm.Text = passBook.Term.ToString("0 tháng");
            partialWithdrawal.txtInterestRates.Text = passBook.InterestRates.ToString("#\\%");
            partialWithdrawal.txtDeposit.Text = passBook.Deposit.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN").NumberFormat);
            partialWithdrawal.txtEndDate.Text = passBook.EndDate.ToString("dd/MM/yyyy");
        }

        public void ShowDialog()
        {
            partialWithdrawal.ShowDialog();
        }
    }
}
