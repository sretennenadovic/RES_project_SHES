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
        #region Fields
        public static ServiceHost sh = new ServiceHost(typeof(SolarPanelImplement));
        public static Dictionary<string, double> panels = new Dictionary<string, double>();
        public static bool ready = false;
        public static ISolarPanelSHES proxy;
        public static ConnectionClass connectionClass = new ConnectionClass();
        #endregion Fields
        static void Main(string[] args)
        {
            connectionClass.OpenConnectionToSHES();
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

        #region DoWork
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
        #endregion DoWork
    }
}
