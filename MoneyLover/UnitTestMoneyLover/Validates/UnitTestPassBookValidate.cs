using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyLover.UI.Validates;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestMoneyLover.Validates
{
    [TestClass]
    public class UnitTestPassBookValidate
    {
        [TestMethod]
        public void SendDateRight()
        {
            PassBookValidate pass = new PassBookValidate();
            var r = pass.IsDateBeforeOrToday("10/28/2019");
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void SendDateWrong()
        {
            PassBookValidate pass = new PassBookValidate();
            var r = pass.IsDateBeforeOrToday("11/16/2020");
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void DepositLessThan1Million()
        {
            PassBookValidate u = new PassBookValidate();
            var r = u.ValidateDeposit(1, 900000);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void DepositRight()
        {
            PassBookValidate u = new PassBookValidate();
            var r = u.ValidateDeposit(1, 2000000);
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void DepositMoreThanWallet()
        {
            PassBookValidate u = new PassBookValidate();
            var r = u.ValidateDeposit(1, 200000000);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void AddDepositWithSettlement()
        {
            PassBookValidate u = new PassBookValidate();
            DateTime dt = DateTime.ParseExact("24/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            MoneyLover.UI.Models.PassBook abc = new MoneyLover.UI.Models.PassBook { BankID = 1, Deposit = 1000000, Due = 3, Term = 1, IndefiniteTerm = 0.07, SentDate = dt };
            var r = u.ValidateAddDeposit(1, 10000, abc);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void AddDepositRight()
        {
            PassBookValidate u = new PassBookValidate();
            DateTime dt = DateTime.ParseExact("15/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ed = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            MoneyLover.UI.Models.PassBook abc = new MoneyLover.UI.Models.PassBook { BankID = 2, Deposit = 2000000, InterestRates = 0.07, IndefiniteTerm = 0.05, SentDate = dt, EndDate = ed, PassBookID = 1, Term = 1, Due = 2 };
            var r = u.ValidateAddDeposit(1, 1500000, abc);
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void AddDepositMoreThanWallet()
        {
            PassBookValidate u = new PassBookValidate();
            DateTime dt = DateTime.ParseExact("24/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            MoneyLover.UI.Models.PassBook abc = new MoneyLover.UI.Models.PassBook { BankID = 1, Deposit = 1000000, Term = 1, Due = 2, IndefiniteTerm = 0.07, SentDate = dt };
            var r = u.ValidateAddDeposit(1, 200000000, abc);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void AddDepositWhenIsNotDue()
        {
            PassBookValidate u = new PassBookValidate();
            DateTime dt = DateTime.ParseExact("24/10/2019", "dd/MM/yyyy", CultureInfo.InvariantCulture);
            MoneyLover.UI.Models.PassBook abc = new MoneyLover.UI.Models.PassBook { BankID = 1, Deposit = 1000000, Term = 1, Due = 2, IndefiniteTerm = 0.07, SentDate = dt };
            var r = u.ValidateAddDeposit(1, 1000000, abc);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EditDepositLessThan1Million()
        {
            PassBookValidate u = new PassBookValidate();
            var r = u.ValidateEditDeposit(1, 1500000, 300000);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EditDepositWrong()
        {
            PassBookValidate u = new PassBookValidate();
            var r = u.ValidateEditDeposit(1, 1500000, 120000000);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EditDepositRight()
        {
            PassBookValidate u = new PassBookValidate();
            var r = u.ValidateEditDeposit(1, 1500000, 90000000);
            Assert.IsTrue(r);
        }
    }
}
