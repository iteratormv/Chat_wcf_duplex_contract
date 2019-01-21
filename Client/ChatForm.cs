using Client.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class ChatForm : Form
    {
        InstanceContext context;
        ChatServiceClient server;
        public string user_name;
        public ChatForm()
        {
            InitializeComponent();
            context = new InstanceContext(new ChatCallback(this));
            server = new ChatServiceClient(context);
            txtMessage.Enabled = false;
            btnSend.Enabled = false;
            lbStatus.Text = "Disconect";
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUser.Text))
            {
                try
                {
                    server.Join(txtUser.Text);
                    user_name = txtUser.Text;
                }
                catch { lbStatus.Text = "Connecting problem"; }
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMessage.Text))
            {
                try
                {
                    server.SendMessage(user_name);
                }
                catch { lbStatus.Text = "Sending problem"; }
            }
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Quit(user_name);
        }
    }
}
