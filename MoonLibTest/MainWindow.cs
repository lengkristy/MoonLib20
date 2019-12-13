using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MoonLib.core;
using MoonLib.core.cmm;

namespace MoonLibTest
{
    public partial class MainWindow : Form, IMessageCallBack
    {

        private IMoonClient moonClient;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            moonClient = ClientFactory.GetDefaultClient();
            moonClient.ConnectServer("127.0.0.1", 8890);
            moonClient.GetCommunicator().RegistServerMessageCallback(this);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtboxUserId.Text))
            {
                MessageBox.Show("输入登陆的用户");
                return;
            }

            moonClient.GetCommunicator().Login(this.txtboxUserId.Text);
        }



        public void ServerMessageHandler(MoonLib.entity.Message message)
        {
            MessageBox.Show("接收到服务端消息:" + message.message_body.content);
        }
    }
}
