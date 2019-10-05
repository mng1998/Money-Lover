﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.Models
{
    public class PassBook
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PassBookID { get; set; }

        [ForeignKey("FKBank")]
        public int BankID { get; set; }
        public virtual Bank FKBank { get; set; }

        [ForeignKey("FKUser")]
        public int UserID { get; set; }
        public virtual User FKUser { get; set; }

        public DateTime SentDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Deposit { get; set; }
        public int Term { get; set; }
        public double InterestRates { get; set; }
        public double IndefiniteTerm { get; set; }
        public int PayInterest { get; set; }
        public int Due { get; set; }
        public double WithDrawalMoney { get; set; }
        public bool Settlement { get; set; }
        public string GetID { get => Bank.GetBankName(BankID) + "_" + PassBookID; }

        public static List<PassBook> getListPassBook(int UserID, int BankID)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                return db.PassBooks.Where(m => m.UserID == UserID && m.BankID == BankID && m.Settlement == false).ToList();
            }
        }
        public static List<PassBook> getListPassBookSettlement(int UserID)
        {
            using (var db = new DB.MoneyLoverDB())
            {
                return db.PassBooks.Where(m => m.UserID == UserID && m.Settlement == true).ToList();
            }
        }
    }
}
