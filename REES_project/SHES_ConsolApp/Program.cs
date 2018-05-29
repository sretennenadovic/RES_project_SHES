using Contracts;
using Contracts.UtilityConnection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    class Program
    {
        #region Proxies
        public static IBattery proxyBattery;
        public static ISolarPanel proxyPanel;
        public static IUtility proxyUtility;
        #endregion Proxies
        #region Dictionaries
        public static Dictionary<string, double> addSolarPanels = new Dictionary<string, double>();
        public static Dictionary<string, double[]> addBatteries = new Dictionary<string, double[]>();
        #endregion Dictionaries
        #region ServiceHosts
        static ServiceHost sh1 = new ServiceHost(typeof(BatterySHESImplement));
        static ServiceHost sh2 = new ServiceHost(typeof(SolarPanelSHESImplement));
        #endregion ServiceHosts
        static Battery battery = new Battery();
        static SolarPanel panel = new SolarPanel();

        //proba
        public static double baterija = 0;
        public static double paneli = 0;

        static void Main(string[] args)
        {
            StartProcesses();
            OpenConnections();

            AddComponents();

            //izmestiti negde
            proxyBattery.ListBatteries(addBatteries, true);
            proxyPanel.listSolarPanels(addSolarPanels, true);
            //izmestiti negde

            DoWork();

            //ovo ce ici u task ili while true
       


            while (true)
            {
                Console.WriteLine("Novaccccccccccccccccccccccccc je:" + proxyUtility.CalculateMoney(baterija+paneli).ToString());
                Thread.Sleep(1000);
            }
            //task

            Console.ReadKey();
        }

        #region Some Job
        private static void DoWork()
        {
            Task t1 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if(DateTime.Now.Hour == 3 || DateTime.Now.Hour == 4 || DateTime.Now.Hour == 5)
                    {
                        proxyBattery.DoWork(0);
                    }
                    else if(DateTime.Now.Hour == 14 || DateTime.Now.Hour == 15 || DateTime.Now.Hour == 16)
                    {
                        proxyBattery.DoWork(1);
                    }
                    else
                    {
                        proxyBattery.DoWork(2);
                    }

                    Thread.Sleep(1000);
                }
            });
        }
        #endregion Some Job

        #region method for adding components in system
        private static void AddComponents()
        {
            battery.Add();
            panel.Add();
        }
        #endregion method for adding components in system

        #region Starting processes
        private static void StartProcesses()
        {
            StartBatteryProcess();
            StartSolarPanelProcess();
            StartUtilityProcess();
        }
        
        private static void StartBatteryProcess()
        {
            // Pokrecemo Battery projekat
            Process p1 = new Process();
            p1.StartInfo.FileName = @"..\..\..\Battery\bin\Debug\Battery.exe";
            p1.Start();
        }

        private static void StartSolarPanelProcess()
        {
            //Pokrecemo SolarPanel projekat
            Process p2 = new Process();
            p2.StartInfo.FileName = @"..\..\..\SolarPanel\bin\Debug\SolarPanel.exe";
            p2.Start();
        }

        private static void StartUtilityProcess()
        {
            //Pokrecemo Utility projekat
            Process p3 = new Process();
            p3.StartInfo.FileName = @"..\..\..\Utility\bin\Debug\Utility.exe";
            p3.Start();
        }
        #endregion Starting processes

        #region Opening connections
        private static void OpenConnections()
        {
            OpenConnectionForBattery();
            OpenConnectionForSolarPanel();
            OpenConnectionForUtility();

        }

        private static void OpenConnectionForBattery()
        {
            //Otvaranje servisa za bateriju
            sh1.AddServiceEndpoint(typeof(IBatterySHES), new NetTcpBinding(), new Uri("net.tcp://localhost:10010/IBatterySHES"));
            sh1.Open();
            
            //Otvaranje kanala ka bateriji
            ChannelFactory<IBattery> cf1 = new ChannelFactory<IBattery>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10020/IBattery"));
            proxyBattery = cf1.CreateChannel();

            Console.WriteLine("Connected with battery!");
        }

        private static void OpenConnectionForUtility()
        {
            //Otvaranje kanala ka utility
            ChannelFactory<IUtility> cf1 = new ChannelFactory<IUtility>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10050/IUtility"));
            proxyUtility = cf1.CreateChannel();

            Console.WriteLine("Connected with Utility!");
        }

        private static void OpenConnectionForSolarPanel()
        {
            //Otvaranje servisa za bateriju
            sh2.AddServiceEndpoint(typeof(ISolarPanelSHES), new NetTcpBinding(), new Uri("net.tcp://localhost:10040/ISolarPanelSHES"));
            sh2.Open();

            //Otvaranje kanala ka bateriji
            ChannelFactory<ISolarPanel> cf1 = new ChannelFactory<ISolarPanel>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10030/ISolarPanel"));
            proxyPanel = cf1.CreateChannel();

            Console.WriteLine("Connected with solar panel!");
        }
        #endregion Opening connections
    }
}
