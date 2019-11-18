using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoneyLover.UI.Validates;

namespace UnitTestMoneyLover.Validates
{
    [TestClass]
    public class UnitTestAccountValidate
    {
        [TestMethod]
        public void EmailBlank()
        {
            AccountValidate addr = new AccountValidate();
            var r = addr.IsValidEmail("");
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EmailNull()
        {
            AccountValidate addr = new AccountValidate();
            var r = addr.IsValidEmail(null);
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EmailWrongFormat()
        {
            AccountValidate addr = new AccountValidate();
            var r = addr.IsValidEmail("user1.com");
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void EmailCorrectFormat()
        {
            AccountValidate addr = new AccountValidate();
            var r = addr.IsValidEmail("user1@moneylover.com");
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void PasswordLessThan8()
        {
            AccountValidate pass = new AccountValidate();
            var r = pass.IsValidPassword("!Money1");
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void PasswordCorrectFormat()
        {
            AccountValidate pass = new AccountValidate();
            var r = pass.IsValidPassword("!Moneylover1");
            Assert.IsTrue(r);
        }

        [TestMethod]
        public void PasswordHasOnlyNumber()
        {
            AccountValidate pass = new AccountValidate();
            var r = pass.IsValidPassword("123456789");
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void PasswordHasOnlyChars()
        {
            AccountValidate pass = new AccountValidate();
            var r = pass.IsValidPassword("Moneylover");
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void PasswordHasOnlySpecialChars()
        {
            AccountValidate pass = new AccountValidate();
            var r = pass.IsValidPassword("!@#$&*!@#$&*");
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void PasswordHasOnlyNumber_Chars()
        {
            AccountValidate pass = new AccountValidate();
            var r = pass.IsValidPassword("Moneylover1");
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void PasswordHasOnlyNumber_SpecialChars()
        {
            AccountValidate pass = new AccountValidate();
            var r = pass.IsValidPassword("!@#$&*12345");
            Assert.IsFalse(r);
        }

        [TestMethod]
        public void PasswordHasOnlyChars_SpecialChars()
        {
            AccountValidate pass = new AccountValidate();
            var r = pass.IsValidPassword("Money!@#$&*");
            Assert.IsFalse(r);
        }
    }
}
