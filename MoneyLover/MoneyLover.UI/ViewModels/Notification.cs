using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLover.UI.ViewModels
{
    public class Notification
    {
        public Views.Notification noti;
        public bool flag = false;

        public Notification(Models.PassBook pb)
        {
            noti = new Views.Notification();

            noti.txtNoti.Text = "Sổ tiết kiệm " + pb.GetID.ToString() + " đến hạn ngày " + pb.EndDate.ToString("dd/MM/yyyy") + ". Số tiền rút ra trước hạn sẽ được tính lãi theo lãi suất không kì hạn (0.05%/năm). Bạn có muốn tiếp tục không?";

            noti.btnNo.Click += (sender, e) =>
            {
                noti.Close();
            };

            noti.btnYes.Click += (sender, e) =>
            {
                flag = true;
                noti.Close();
            };
        }

        public void ShowDialog()
        {
            noti.ShowDialog();
        }
    }
}
