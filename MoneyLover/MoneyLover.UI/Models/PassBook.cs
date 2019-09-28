using System;
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
        public string PassBookID { get; set; }

        [ForeignKey("FKBank")]
        public int BankID { get; set; }
        public virtual Bank FKBank { get; set; }

        [ForeignKey("FKUser")]
        public int UserID { get; set; }
        public virtual User FKUser { get; set; }

        public DateTime SentDate { get; set; }
        public float Deposit { get; set; }
        public int Term { get; set; }
        public float InterestRates { get; set; }
        public float IndefiniteTerm { get; set; }
        public string PayInterest { get; set; }
        public string Due { get; set; }
    }
}
