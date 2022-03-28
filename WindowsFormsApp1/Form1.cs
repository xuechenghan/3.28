using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        UdpClient U;
        Thread Th;
        //監聽副程序
        private void Listen()
        {   //設定監聽用的通訊阜
            int Port = int.Parse(textBox1_listenPort.Text);
            //監聽UDP監聽器實體
            U = new UdpClient(Port);
            //建立本機端點資訊
            IPEndPoint EP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), Port);

            while(true)
            {
                byte[] B = U.Receive(ref EP);
                textBox2_receiveMsg.Text = Encoding.Default.GetString(B);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Th = new Thread(Listen);

            Th.Start();

            button1.Enabled = false;
        }
        private void Forml_FormClosing(object sender,FormClosingEventArgs e)
        {
            try 
            {
                Th.Abort();  //關閉監聽執行續
                U.Close();   //關閉監聽器
            }
            catch
            {
                //忽略錯誤
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string IP = textBox_targetIP.Text;  //設定發送目標IP
            int Port = int.Parse(textBox_targetPort.Text);//設定發送目標Port
            byte[] B = Encoding.Default.GetBytes(textBox_sendMsg.Text);
            UdpClient S = new UdpClient();
            S.Send(B, B.Length, IP, Port);
            S.Close();
        }
        private string MyIP()
        {
            string hostname = Dns.GetHostName();
            IPAddress[] ip = Dns.GetHostEntry(hostname).AddressList;

            foreach(IPAddress it in ip)
            {
                if(it.AddressFamily == AddressFamily.InterNetwork && it.ToString() != "192.168.56.1")
                {
                    return it.ToString();
                }
                
            }
            return "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           this.Text = "我的IP:" + MyIP();
        }
    }
}
