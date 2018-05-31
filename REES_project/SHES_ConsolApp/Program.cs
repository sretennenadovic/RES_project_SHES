using Contracts;
using Contracts.ConsumerConnection;
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
        public static IConsumer proxyConsumer;
        #endregion Proxies
        #region Dictionaries
        public static Dictionary<string, double> addSolarPanels = new Dictionary<string, double>();
        public static Dictionary<string, double[]> addBatteries = new Dictionary<string, double[]>();
        public static Dictionary<string, double> addConsumers = new Dictionary<string, double>();
        #endregion Dictionaries
        #region ServiceHosts
        static ServiceHost sh1 = new ServiceHost(typeof(BatterySHESImplement));
        static ServiceHost sh2 = new ServiceHost(typeof(SolarPanelSHESImplement));
        static ServiceHost sh3 = new ServiceHost(typeof(ConsumerSHESImplement));
        #endregion ServiceHosts
        public enum States : int { PUNJENJE = 0, PRAZNJENJE = 1, ISKLJUCENA = 2 };
        public static States state = States.ISKLJUCENA;
        public static Object obj = new object();

        static Battery battery = new Battery();
        static SolarPanel panel = new SolarPanel();
        static Consumer consumer = new Consumer();

        #region PomValues
        public static double batteryPom = 0;
        public static double panelPom = 0;
        public static double consumerPom = 0;
        public static double price = 0;
        #endregion
        static void Main(string[] args)
        {
            StartProcesses();
            OpenConnections();

            AddComponents();

            //izmestiti negde
            proxyBattery.ListBatteries(addBatteries, true);
            proxyPanel.listSolarPanels(addSolarPanels, true);
            proxyConsumer.ListConsumers(addConsumers, true);
            //izmestiti negde

            DoWork();
            Count();
            //ovo ce ici u task ili while true


            Console.ReadKey();
        }

        private static void Count()
        {
            Task t = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    if (batteryPom != 0)
                    {
                        

                        double value = 0;

                        double panelPom2 = panelPom;
                        double consumerPom2 = consumerPom;
                        double batteryPom2 = batteryPom;

                        lock (obj)
                        {
                            panelPom = 0;
                            consumerPom = 0;
                            batteryPom = 0;
                        }

                        Console.WriteLine(state.ToString());

                        if (state == States.ISKLJUCENA)
                        {
                            value = panelPom2 - consumerPom2;
                            price = proxyUtility.CalculateMoney(value);
                            Console.WriteLine("Panel: " + panelPom2 + " baterija: " + batteryPom2 + " potrosaci: " + consumerPom2);
                        }
                        else if (state == States.PRAZNJENJE)
                        {
                            value = panelPom2 + batteryPom2 - consumerPom2;
                            price = proxyUtility.CalculateMoney(value);
                            //Console.WriteLine("Panel: " + panelPom2 + " baterija: " + batteryPom2 + " potrosaci: " + consumerPom2);
                        }
                        else
                        {
                            value = panelPom2 - batteryPom2 - consumerPom2;
                            price = proxyUtility.CalculateMoney(value);
                            //Console.WriteLine("Panel: " + panelPom2 + " baterija: " + batteryPom2 + " potrosaci: " + consumerPom2);
                        }

                        Console.WriteLine("Trenutna cena je: " + price);
                    }
                }
            });
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
            consumer.Add();
        }
        #endregion method for adding components in system

        #region Starting processes
        private static void StartProcesses()
        {
            StartBatteryProcess();
            StartSolarPanelProcess();
            StartUtilityProcess();
            StartConsumerProcess();
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

        private static void StartConsumerProcess()
        {
            //Pokrecemo Consumer projekat
            Process p4 = new Process();
            p4.StartInfo.FileName = @"..\..\..\Consumer\bin\Debug\Consumer.exe";
            p4.Start();
        }
        #endregion Starting processes

        #region Opening connections
        private static void OpenConnections()
        {
            OpenConnectionForBattery();
            OpenConnectionForSolarPanel();
            OpenConnectionForUtility();
            OpenConnectionForConsumer();

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
            //Otvaranje servisa za Solarni panel
            sh2.AddServiceEndpoint(typeof(ISolarPanelSHES), new NetTcpBinding(), new Uri("net.tcp://localhost:10040/ISolarPanelSHES"));
            sh2.Open();

            //Otvaranje kanala ka Solarnom panelu
            ChannelFactory<ISolarPanel> cf1 = new ChannelFactory<ISolarPanel>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10030/ISolarPanel"));
            proxyPanel = cf1.CreateChannel();

            Console.WriteLine("Connected with solar panel!");
        }

        private static void OpenConnectionForConsumer()
        {
            //Otvaranje servisa za Consumera
            sh3.AddServiceEndpoint(typeof(IConsumerSHES), new NetTcpBinding(), new Uri("net.tcp://localhost:10070/IConsumerSHES"));
            sh3.Open();

            //Otvaranje kanala ka Consumeru
            ChannelFactory<IConsumer> cf1 = new ChannelFactory<IConsumer>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10060/IConsumer"));
            proxyConsumer = cf1.CreateChannel();

            Console.WriteLine("Connected with consumer!");
        }
        #endregion Opening connections
    }
}
