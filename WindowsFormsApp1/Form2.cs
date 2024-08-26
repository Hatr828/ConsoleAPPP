using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

public partial class Form2 : Form
{
    private Socket tcpClient;
    private string userName;

    public Form2()
    {
        InitializeComponent();
    }

    public Form2(Socket clientSocket, string username) : this()
    {
        tcpClient = clientSocket;
        userName = username;

        BeginReceiveMessages();
    }


    private void buttonSend_Click(object sender, EventArgs e)
    {
        string message = textBoxMessage.Text.Trim();

        if (!string.IsNullOrEmpty(message))
        {
            string fullMessage = $"{userName}: {message}";
            byte[] buffer = Encoding.UTF8.GetBytes(fullMessage);

            tcpClient.Send(buffer);

            listViewChat.Items.Add(new ListViewItem(fullMessage));
            textBoxMessage.Clear();
        }
    }

    private void BeginReceiveMessages()
    {
        try
        {
            byte[] buffer = new byte[1024];
            tcpClient.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(OnReceive), buffer);
        }
        catch (SocketException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void OnReceive(IAsyncResult ar)
    {
        try
        {
            byte[] buffer = (byte[])ar.AsyncState;
            int bytesReceived = tcpClient.EndReceive(ar);

            if (bytesReceived > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesReceived);
                ListView.Invoke((MethodInvoker)(() => listViewChat.Items.Add(new ListViewItem(message))));

                BeginReceiveMessages();
            }
        }
        catch (SocketException ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Form2_FormClosed(object sender, FormClosedEventArgs e)
    {
        if (tcpClient != null)
        {
            tcpClient.Close();
        }

        Application.Exit(); 
    }
        
}
