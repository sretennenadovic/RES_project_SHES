using Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    class Program
    {
        public static IBattery proxy;
        public static ISolarPanel proxyB;
        public static Dictionary<string, double> addSolarPanels = new Dictionary<string, double>();
        public static Dictionary<string, double[]> addBatteries = new Dictionary<string, double[]>();
        static ServiceHost sh1 = new ServiceHost(typeof(BatterySHESImplement));
        static ServiceHost sh2 = new ServiceHost(typeof(SolarPanelSHESImplement));
        static Battery battery = new Battery();

        static void Main(string[] args)
        {
            StartProcesses();
            OpenConnections();

            AddComponents();

            if (DateTime.Now.Hour == 20)
            {
                proxy.listBatteries(addBatteries, true);
                proxy.DoWork(1);
            }
            //menjacemo da bude lepse
            Console.WriteLine("Ispis3");
            addSolarPanels = new Dictionary<string, double>();
            addSolarPanels.Add("solar1", 10);
            addSolarPanels.Add("solar2", 15);

            proxyB.listSolarPanels(addSolarPanels, true);

            Console.ReadKey();
        }

        #region method for adding components in system
        private static void AddComponents()
        {
            battery.Add();
        }
        #endregion method for adding components in system

        #region Starting processes
        private static void StartProcesses()
        {
            // Pokrecemo Battery projekat
            Process p1 = new Process();
            p1.StartInfo.FileName = @"..\..\..\Battery\bin\Debug\Battery.exe";
            p1.Start();
            
            //Pokrecemo SolarPanel projekat
            Process p2 = new Process();
            p2.StartInfo.FileName = @"..\..\..\SolarPanel\bin\Debug\SolarPanel.exe";
            p2.Start();
        }
        #endregion Starting processes

        #region Opening connections
        private static void OpenConnections()
        {
            OpenConnectionForBattery();
            OpenConnectionForSolarPanel();

        }

        private static void OpenConnectionForBattery()
        {
            //Otvaranje servisa za bateriju
            sh1.AddServiceEndpoint(typeof(IBatterySHES), new NetTcpBinding(), new Uri("net.tcp://localhost:10010/IBatterySHES"));
            sh1.Open();
            
            //Otvaranje kanala ka bateriji
            ChannelFactory<IBattery> cf1 = new ChannelFactory<IBattery>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10020/IBattery"));
            proxy = cf1.CreateChannel();

            Console.WriteLine("Otvorena konekcija sa baterijom!");
        }

        private static void OpenConnectionForSolarPanel()
        {
            //Otvaranje servisa za bateriju
            sh2.AddServiceEndpoint(typeof(ISolarPanelSHES), new NetTcpBinding(), new Uri("net.tcp://localhost:10040/ISolarPanelSHES"));
            sh2.Open();

            //Otvaranje kanala ka bateriji
            ChannelFactory<ISolarPanel> cf1 = new ChannelFactory<ISolarPanel>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10030/ISolarPanel"));
            proxyB = cf1.CreateChannel();

            Console.WriteLine("Otvorena konekcija sa solarnim panelom!");
        }
        #endregion Opening connections
    }
}
