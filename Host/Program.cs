using Host.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            Type chat_service = typeof(ChatService);
            ServiceHost host = new ServiceHost(chat_service);
            host.Open();
            Console.WriteLine("Chat service run ");
            Console.ReadKey();
            host.Close();
        }
    }
}
