using Contracts.ConsumerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        #region Fields
        public static bool ready = false; 
        public static ServiceHost sh = new ServiceHost(typeof(ConsumerImplement));
        public static IConsumerSHES proxy;
        public static Dictionary<string, double> consumers = new Dictionary<string, double>();
        public static ConnectionClass connectionClass = new ConnectionClass();
        #endregion Fields

        static void Main(string[] args)
        {
            connectionClass.OpenConnectionToSHES();
            while (true)
            {
                if (ready)
                {
                    DoWork();
                    break;
                }
            }
            Console.ReadLine();
        }

        #region Work
        private static void DoWork()
        {
            Task t1 = Task.Factory.StartNew(() =>
            {
                while(true)
                {
                    double retVal = 0;
                    foreach (double item in consumers.Values)
                    {
                        retVal += item;
                    }
                    proxy.MyInfo(retVal);

                    Thread.Sleep(1000);
                }
            });
        }
        #endregion Work
    }
}
