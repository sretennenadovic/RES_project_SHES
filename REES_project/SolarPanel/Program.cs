using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SolarPanel
{
    class Program
    {
        static ServiceHost sh = new ServiceHost(typeof(SolarPanelImplement));
        public static Dictionary<string, double> panels = new Dictionary<string, double>();
        public static bool ready = false;
        public static ISolarPanelSHES proxy;
        static void Main(string[] args)
        {
            OpenConnectionToSHES();
            while (true)
            {
                if (ready)
                {
                    RadiPosao();
                    break;
                }
            }

            Console.ReadKey(); 
        }

        private static void OpenConnectionToSHES()
        {
            //dizemo servis na bateriji
            sh.AddServiceEndpoint(typeof(ISolarPanel), new NetTcpBinding(), new Uri("net.tcp://localhost:10030/ISolarPanel"));
            sh.Open();

            //konekcija ka serveru shes2
            ChannelFactory<ISolarPanelSHES> cf1 = new ChannelFactory<ISolarPanelSHES>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10040/ISolarPanelSHES"));
            proxy = cf1.CreateChannel();

            Console.WriteLine("Otvorena konekcija prema SHES-u!");
        }

        private static void RadiPosao()
        {
            Task t1 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    double rez = 0;
                    foreach (KeyValuePair<string, double> item in panels)
                    {
                        if((DateTime.Now.Hour >= 20 && DateTime.Now.Hour <= 24) || DateTime.Now.Hour < 6)
                        {
                            rez = 0;
                        }
                        else if(DateTime.Now.Hour >= 6 && DateTime.Now.Hour < 8)
                        {
                            rez += (item.Value * 20) / 100;
                        }
                        else if(DateTime.Now.Hour >= 8 && DateTime.Now.Hour < 11)
                        {
                            rez += (item.Value * 50) / 100;
                        }
                        else if(DateTime.Now.Hour >= 11 && DateTime.Now.Hour < 16)
                        {
                            rez += item.Value;
                        }
                        else if(DateTime.Now.Hour >= 16 && DateTime.Now.Hour < 18)
                        {
                            rez += (item.Value * 50) / 100;
                        }
                        else if(DateTime.Now.Hour >= 18 && DateTime.Now.Hour < 20)
                        {
                            rez += (item.Value * 20) / 100;
                        }
                    }
                    try
                    {
                        proxy.MyInfo(rez);
                    }catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
