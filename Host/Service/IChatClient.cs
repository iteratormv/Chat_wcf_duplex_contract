using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host.Service
{
    [ServiceContract]
    interface IChatClient
    {
        [OperationContract(IsOneWay = true)]
        void ResiveMessage(string user, string message);
        [OperationContract(IsOneWay = true)]
        void GetUsers(List<string> users);
    }
}
