using Client;
using Client.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ChatCallback : IChatServiceCallback
    {
        ChatForm f;
        public ChatCallback(ChatForm _f) {f = _f;}

        public void GetUsers(string[] users)
        {
            f.Invoke(new Action(() => f.lbxUsers.DataSource = users));
            f.Invoke(new Action(() => f.lbStatus.Text = "Connected as " + f.user_name));
            f.Invoke(new Action(() => f.btnSend.Enabled = true));
            f.Invoke(new Action(() => f.txtMessage.Enabled = true));
        }

        public void ResiveMessage(string user, string message)
        {
            f.Invoke(new Action(() => f.txtChat.AppendText(user + ":" + message+"\n")));
        }

    }
}
