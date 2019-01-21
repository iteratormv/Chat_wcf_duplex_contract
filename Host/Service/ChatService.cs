using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, 
        InstanceContextMode = InstanceContextMode.Single)]
    public class ChatService:IChatService
    {
        Dictionary<IChatClient, string> _users = new Dictionary<IChatClient, string>();

        public void Join(string username)
        {
            var exists = from string e in this._users.Values
                         where e == username select e;

            if (exists.Count() == 0)
            {
                var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
                _users[connection] = username;
                string user;
                if (!_users.TryGetValue(connection, out user)) return ;
                foreach (var u in _users.Keys)
                {
                    var uss = _users.Values.ToList();
                    u.GetUsers(uss);
                }
            }       
        }

        public void Quit(string username)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            _users.Remove(connection);
            foreach (var u in _users.Keys)
            {
                var uss = _users.Values.ToList();
                u.GetUsers(uss);
            }
        }

        public void SendMessage(string message)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            string user;
            if (!_users.TryGetValue(connection, out user)) return;
            foreach(var other in _users.Keys)
            {
                if (other == connection) {
                    other.ResiveMessage("Вы: ", message);
                    continue;
                }
                other.ResiveMessage(user, message);
            }
        }
    }
}
