using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Battery
{
    class Program
    {
        public enum States : int { PUNJENJE=0,PRAZNJENJE=1,ISKLJUCENA=2};

        static ServiceHost sh = new ServiceHost(typeof(BatteryImplement));
        public static IBatterySHES proxy;
        public static Dictionary<string, double[]> batteries = new Dictionary<string, double []>();
        public static bool ready = false;
        public static int counter = 0;
        public static States state = States.ISKLJUCENA;
        static object _lock = new object();


        static void Main(string[] args)
        {
            OpenConnectionToSHES();

            while (true)
            {
                if (ready)
                {
                    RadiPosao();
                    OdrzavajStanje();
                    break;
                }
            }

            Console.ReadKey();

        }

        private static void OpenConnectionToSHES()
        {
            //dizemo servis na bateriji
            sh.AddServiceEndpoint(typeof(IBattery), new NetTcpBinding(), new Uri("net.tcp://localhost:10020/IBattery"));
            sh.Open();

            //konekcija ka serveru shes2
            ChannelFactory<IBatterySHES> cf1 = new ChannelFactory<IBatterySHES>(new NetTcpBinding(), new EndpointAddress("net.tcp://localhost:10010/IBatterySHES"));
            proxy = cf1.CreateChannel();

            Console.WriteLine("Otvorena konekcija prema SHES-u!");
        }

        private static void OdrzavajStanje()
        {
            Task t1 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (state == States.PUNJENJE)
                    {
                        counter++;
                        if (counter % 60 == 0 && counter != 0)
                        {
                            counter = 0;

                            foreach (var item in batteries)
                            {
                                //ne mozemo napuniti bateriju vise nego sto joj je max snaga
                                if (item.Value[0] < item.Value[1])
                                {
                                    lock (_lock)
                                    {
                                        //pretvorimo sate kapaciteta u minute, uvecamo za 1, a zatim vratimo u sate
                                        item.Value[0] = item.Value[0] * 60;
                                        item.Value[0] += 1;
                                        item.Value[0] = item.Value[0] / 60;
                                    }
                                }
                            }
                        }
                    }
                    else if (state == States.PRAZNJENJE)
                    {
                        counter++;
                        if (counter % 60 == 0 && counter != 0)
                        {
                            counter = 0;

                            foreach (var item in batteries)
                            {
                                //ne mozemo isprazniti bateriju vise od 0
                                if (item.Value[0] > 0)
                                {
                                    lock (_lock)
                                    {
                                        //pretvorimo sate kapaciteta u minute, umanjimo za 1, a zatim vratimo u sate
                                        item.Value[0] = item.Value[0] * 60;
                                        item.Value[0] -= 1;
                                        item.Value[0] = item.Value[0] / 60;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        counter = 0;
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        //metoda koja ce se vrteti na svaki sekund i slati odgovarajuce informacije serveru (SHES-u)
        private static void RadiPosao()
        {
            Task t1 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    double rez = 0;
                    foreach (KeyValuePair<string,double[]> item in batteries)
                    {
                        lock(_lock){
                        //prvo u listi se nalazi KAPACITET, i samo njega sabiramo
                        rez += item.Value[0];
                        }
                    }

                    proxy.MyInfo(rez, (int)state);
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
