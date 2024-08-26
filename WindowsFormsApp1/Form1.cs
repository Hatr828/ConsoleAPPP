using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Socket tcpClient;

        public Form1()
        {
            InitializeComponent();
            ConnectWithServer();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                userName = label1.Text,
                password = label2.Text,
                email = label3.Text
            };

            string loginRequest = $"LOGIN|{user.userName}|{user.password}|{user.email}";
            byte[] buffer = Encoding.UTF8.GetBytes(loginRequest);

            tcpClient.Send(buffer);

            byte[] responseBuffer = new byte[1024];
            int bytesReceived = tcpClient.Receive(responseBuffer);
            string response = Encoding.UTF8.GetString(responseBuffer, 0, bytesReceived);

            if (response == "SUCCESS")
            {
                Form2 form2 = new Form2();
                form2.Show();
                this.Hide();

                tcpClient.Close();
            }
            else
            {
                MessageBox.Show("Don`t find");
            }
        }

        public void ConnectWithServer()
        {
            try
            {
                tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                tcpClient.Connect("127.0.0.1", 8888);
            }
            catch (SocketException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            tcpClient.Close();
        }
    }
}
