using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using MoonLib.core;
using MoonLib.util;

namespace MQClient
{
    public partial class LoginForm : CCSkinMain
    {
        public static IMoonClient moonClient = null;

        public string ServerIP { get; set; }

        public string ServerPort { get; set; }

        public LoginForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            
        }

        private void SBtnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ServerIP))
            {
                MessageBox.Show("请配置服务器IP");
                return;
            }
            if (string.IsNullOrEmpty(this.ServerPort))
            {
                MessageBox.Show("请配置服务器端口");
                return;
            }
            try
            {
                moonClient = ClientFactory.GetDefaultClient();
                moonClient.GetCommunicator().RegistServerMessageCallback(new MoonMessageCallBack());
                moonClient.ConnectServer(UUIDUtil.Generator32UUID(), ServerIP, Int32.Parse(ServerPort));
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接服务器失败：" + ex.Message);
            }

        }

        private void SBtnSetting_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting(this);
            setting.ShowDialog();
        }
    }
}
