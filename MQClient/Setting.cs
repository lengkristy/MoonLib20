using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;

namespace MQClient
{
    public partial class Setting : CCSkinMain
    {
        private LoginForm loginForm;

        public Setting(LoginForm loginForm)
        {
            InitializeComponent();
            this.loginForm = loginForm;
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void StbOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(StbServerIP.Text))
            {
                MessageBox.Show("请输入IP");
                return;
            }
            if (string.IsNullOrEmpty(StbServerPort.Text))
            {
                MessageBox.Show("请输入端口");
                return;
            }
            loginForm.ServerIP = StbServerIP.Text;
            loginForm.ServerPort = StbServerPort.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
