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
        static Dictionary<string, double[]> addBatteries;
        static Dictionary<string, double> addSolarPanels;
        static ServiceHost sh1 = new ServiceHost(typeof(BatterySHESImplement));
        static ServiceHost sh2 = new ServiceHost(typeof(SolarPanelSHESImplement));

        static void Main(string[] args)
        {
            StartProcesses();
            OpenConnections();


            //menjacemo da bude lepse 
            addBatteries = new Dictionary<string, double []>();
            addBatteries.Add("duracel", new double[2] { 3, 4 });
            addBatteries.Add("sssss", new double[2] { 1, 3 });
            addBatteries.Add("varta", new double[2] { 2, 4 });
            if (DateTime.Now.Hour == 18)
            {
                proxy.listBatteries(addBatteries, true);
                proxy.DoWork(1);
            }
            //menjacemo da bude lepse

            addSolarPanels = new Dictionary<string, double>();
            addSolarPanels.Add("solar1", 10);
            addSolarPanels.Add("solar2", 15);

            proxyB.listSolarPanels(addSolarPanels, true);

            Console.ReadKey();
        }


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
    }
}
