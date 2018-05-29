using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanel
{
    class ConnectionClass
    {

        public void OpenConnectionToSHES()
        {
            //dizemo servis na bateriji
            Program.sh.AddServiceEndpoint(typeof(ISolarPanel), new NetTcpBinding(), new Uri("net.tcp://localhost:10030/ISolarPanel"));
            Program.sh.Open();

            //konekcija ka serveru shes2
            ChannelFactory<ISolarPanelSHES> cf1 = new ChannelFactory<ISolarPanelSHES>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10040/ISolarPanelSHES"));
            Program.proxy = cf1.CreateChannel();

            Console.WriteLine("Otvorena konekcija prema SHES-u!");
        }
    }
}
