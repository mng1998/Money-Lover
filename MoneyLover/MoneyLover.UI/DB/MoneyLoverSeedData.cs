using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoneyLover.UI.Models;

namespace MoneyLover.UI.DB
{
    public class MoneyLoverSeedData : DropCreateDatabaseIfModelChanges<MoneyLoverDB>
    {
        protected override void Seed(MoneyLoverDB db)
        {
            db.Users.Add(new User { Email = "user1@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });
            db.Users.Add(new User { Email = "user2@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });
            db.Users.Add(new User { Email = "user3@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });
            db.Users.Add(new User { Email = "user4@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });
            db.Users.Add(new User { Email = "user5@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });
            db.Users.Add(new User { Email = "user6@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });
            db.Users.Add(new User { Email = "user7@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });
            db.Users.Add(new User { Email = "user8@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });
            db.Users.Add(new User { Email = "user9@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });
            db.Users.Add(new User { Email = "user10@moneylover.com", Password = "!Moneylover", Wallet = 100000000, SavingsWallet = 0 });

            db.Banks.Add(new Bank { BankName = "Ngân hàng Á Châu", ShortName = "ACB" });
            db.Banks.Add(new Bank { BankName = "Ngân hàng Tiên Phong", ShortName = "TPBank" });
            db.Banks.Add(new Bank { BankName = "Ngân hàng Kỹ Thương Việt Nam", ShortName = "TCB" });
            db.Banks.Add(new Bank { BankName = "Ngân hàng Việt Nam Thịnh Vượng", ShortName = "VPBank" });
            db.Banks.Add(new Bank { BankName = "Ngân hàng TMCP Quốc tế", ShortName = "VIB" });
            db.Banks.Add(new Bank { BankName = "Ngân hàng Sài Gòn Thương Tín", ShortName = "Sacombank" });
            db.Banks.Add(new Bank { BankName = "Ngân hàng Việt Nam Thương Tín", ShortName = "VietBank" });
            db.Banks.Add(new Bank { BankName = "Ngân hàng Đầu tư và Phát triển Việt Nam", ShortName = "BIDV" });
            db.Banks.Add(new Bank { BankName = "Ngân hàng Công Thương Việt Nam", ShortName = "VCB" });
            db.Banks.Add(new Bank { BankName = "Ngân hàng Ngoại thương Việt Nam", ShortName = "VCB" });

            db.SaveChanges();
        }
    }
}
