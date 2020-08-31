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
    public partial class FrmLogin : CCSkinMain
    {
        private string _username;
        private string _password;

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this._username = this.userName.Text;
            this._password = this.password.Text;
            if (string.IsNullOrEmpty(this._username))
            {
                MessageBox.Show("请输入用户名");
                return;
            }
            if (string.IsNullOrEmpty(this._password))
            {
                MessageBox.Show("请输入密码");
                return;
            }
            this.Close();
        }

        public string GetUsername()
        {
            return this._username;
        }

        public string GetPassword()
        {
            return this._password;
        }
    }
}
