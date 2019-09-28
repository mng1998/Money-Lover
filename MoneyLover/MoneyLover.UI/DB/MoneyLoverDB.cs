using MoneyLover.UI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.DB
{
    public class MoneyLoverDB : DbContext
    {
        public MoneyLoverDB() : base("MoneyLover") { }
        public DbSet<User> Users { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<PassBook> PassBooks { get; set; }
    }
}
