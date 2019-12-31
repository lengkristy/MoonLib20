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

namespace MoonLibTest
{
    public partial class MainWindow : Form, IMessageCallBack
    {

        private List<IMoonClient> moonClient = new List<IMoonClient>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            
        }


        public void ServerMessageHandler(MoonLib.entity.Message message)
        {
            if (message.message_head.main_msg_num == MoonProtocol.ServeClientInfo.SYS_MAIN_PROTOCOL_SCI)
            {
                this.Invoke(new Action<String>((data) =>
                {
                    this.listBoxMsg.Items.Add(data);
                }),"有新消息到来");
                
                this.Invoke(new Action<JArray>((data) =>
                {
                    if (data.Count > 0)
                    {
                        string clients = string.Empty;
                        for (int i = 0; i < data.Count; i++)
                        {
                            clients += data[i].ToString() + ",";
                        }
                        this.listBoxMsg.Items.Add(clients);
                    }
                }), message.message_body.content);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            

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
            client.GetCommunicator().RegistServerMessageCallback(this);
            client.ConnectServer(UUIDUtil.Generator32UUID(), "127.0.0.1", 8890);
            while (true)
            {
                System.Threading.Thread.Sleep(5000);
                ////成功之后，向服务端发起获取所有客户端列表
                client.GetCommunicator().GetServerClientInfoList();
            }
        }
    }
}
