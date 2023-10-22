using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        // 각 소켓의 변수 선언
        ScreenSocket ss;
        TextSocket ts;
        FileSocket fs;

        public Server()
        {
            InitializeComponent();
        }

        // Start버튼의 클릭 시 동작
        private void start_bnt_Click(object sender, EventArgs e)
        {
            // 폼의 컴포넌트 동작
            start_bnt.Enabled = false;
            end_btn.Enabled = true;
            state_label.Text = "Server Open";
            // 각 소켓을 정의하여 시작
            ss = new ScreenSocket(this);
            ts = new TextSocket(this);
            fs = new FileSocket(this);
        }

        // End버튼의 클릭 시 동작
        private void end_btn_Click(object sender, EventArgs e)
        {
            // 알림창으로 종료 메세지를 띄워준다.
            Notification_EXIT notify = new Notification_EXIT();
            notify.ShowDialog();

            // 각 소켓을 닫고 폼을 종료한다.
            ss.exit();
            ts.exit();
            fs.exit();

            this.Close();
        }
        
    }
}
