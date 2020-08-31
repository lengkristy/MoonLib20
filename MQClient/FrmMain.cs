using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CCWin;
using MoonLib.core;
using MQClient.Entity;
using MoonLib.util;

namespace MQClient
{
    /// <summary>
    /// 客户端主窗体
    /// </summary>
    public partial class FrmMain : CCSkinMain
    {
        IMoonClient moonClient;

        private User _user;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            moonClient = ClientFactory.GetDefaultClient();
            _user = new User();
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.ShowDialog();
            _user.Name = frmLogin.GetUsername();
            _user.Password = frmLogin.GetPassword();
            _user.Id = UUIDUtil.Generator32UUID();
            moonClient.ConnectServer(_user.Id, "127.0.0.1", 8891);
        }
    }
}
