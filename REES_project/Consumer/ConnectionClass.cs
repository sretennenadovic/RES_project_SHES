using Contracts.ConsumerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class ConnectionClass
    {
        public void OpenConnectionToSHES()
        {
            //dizemo servis na bateriji
            Program.sh.AddServiceEndpoint(typeof(IConsumer), new NetTcpBinding(), new Uri("net.tcp://localhost:10060/IConsumer"));
            Program.sh.Open();

            //konekcija ka serveru shes2
            ChannelFactory<IConsumerSHES> cf1 = new ChannelFactory<IConsumerSHES>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10070/IConsumerSHES"));
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
