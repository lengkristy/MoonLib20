using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MoonLib.core;
using MoonLib.core.cmm;
using MoonLib.entity;
using MoonLib.util;
using Newtonsoft.Json.Linq;
using System.Threading;
using MoonLib.core.cmm.callback;

namespace MoonLibTest
{
    public partial class MainWindow : Form, SysMessageCallback
    {
        private string ip;

        private string port;

        private List<IMoonClient> moonClient = new List<IMoonClient>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            this.ip = this.txbIP.Text;
            this.port = this.txbPort.Text;

            int count = Int32.Parse(this.txtBobCount.Text);

            for (int i = 0; i < count; i++)
            {
                Thread thread = new Thread(client_thread);
                thread.IsBackground = true;
                thread.Start();
                System.Threading.Thread.Sleep(100);
            }
        }


        //客户端线程
        private void client_thread()
        {
            IMoonClient client = ClientFactory.GetDefaultClient();
            moonClient.Add(client);
            client.GetCommunicator().RegistCallback(this, null, null);
            client.ConnectServer(UUIDUtil.Generator32UUID(), this.ip, int.Parse(this.port));
            while (true)
            {
                ////成功之后，向服务端发起获取所有客户端列表
                System.Threading.Thread.Sleep(5000);
                client.GetCommunicator().GetServerClientInfoList();
                //client.GetCommunicator().SendTextMessageToUser("124354","2132哈哈");
                System.Threading.Thread.Sleep(10000);
            }
        }

        public void RecvServerNodeAllOnlineClientList(string clientList)
        {
            LogUtil.Info("收到服务端消息：", clientList);
        }
    }
}
