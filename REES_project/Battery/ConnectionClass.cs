using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Battery
{
    class ConnectionClass
    {
        public void OpenConnectionToSHES()
        {
            //dizemo servis na bateriji
            Program.sh.AddServiceEndpoint(typeof(IBattery), new NetTcpBinding(), new Uri("net.tcp://localhost:10020/IBattery"));
            Program.sh.Open();

            //konekcija ka serveru shes2
            ChannelFactory<IBatterySHES> cf1 = new ChannelFactory<IBatterySHES>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10010/IBatterySHES"));
            try
            {
                Program.proxy = cf1.CreateChannel();
                Console.WriteLine("Otvorena konekcija prema SHES-u!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
