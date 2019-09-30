using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.ViewModels
{
    public class PassbookList
    {
        private Views.PassbookList passBookList;

        public PassbookList()
        {
            passBookList = new Views.PassbookList();
        }

        public void Show()
        {
            passBookList.Show();
        }
    }
}
