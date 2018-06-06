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

        #region Fileds
        public enum States : int { PUNJENJE=0,PRAZNJENJE=1,ISKLJUCENA=2};

        public static ServiceHost sh = new ServiceHost(typeof(BatteryImplement));
        public static IBatterySHES proxy;
        public static Dictionary<string, Tuple<double,double[]>> batteries = new Dictionary<string, Tuple<double,double[]>>();
        public static bool ready = false;
        public static int counter = 0;
        public static States state = States.ISKLJUCENA;
        static object _lock = new object();

        public static ConnectionClass connectionClass = new ConnectionClass();

        public static double[] storage;
        #endregion Fileds

        static void Main(string[] args)
        {
            connectionClass.OpenConnectionToSHES();

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

        #region KeepState
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
                                if (item.Value.Item1 > item.Value.Item2[0])
                                {

                                    lock (_lock)
                                    {
                                        //pretvorimo sate kapaciteta u minute, uvecamo za 1, a zatim vratimo u sate
                                        item.Value.Item2[0] = item.Value.Item2[0] * 60;
                                        item.Value.Item2[0] += 1;
                                        item.Value.Item2[0] = item.Value.Item2[0] / 60;
                                    }
                                }
                                else
                                {
                                    state = States.ISKLJUCENA;
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
                                if (item.Value.Item2[0] >= 0)
                                {
                                    //ne mozemo is[razniti bateriju manje od 0
                                    lock (_lock)
                                    {
                                        //pretvorimo sate kapaciteta u minute, uvecamo za 1, a zatim vratimo u sate
                                        item.Value.Item2[0] = item.Value.Item2[0] * 60;
                                        item.Value.Item2[0] -= 1;
                                        item.Value.Item2[0] = item.Value.Item2[0] / 60;
                                    }
                                }
                                else
                                {
                                    state = States.ISKLJUCENA;
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
        #endregion KeepState

        #region DoWork

        //metoda koja ce se vrteti na svaki sekund i slati odgovarajuce informacije serveru (SHES-u)
        private static void RadiPosao()
        {
            Task t1 = Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    double rez = 0;
                    foreach (double item in storage)
                    {
                        if (state != States.ISKLJUCENA)
                        {
                            lock (_lock)
                            {
                                rez += item;
                            }
                        }
                        Console.WriteLine("Vrednost je; " + item);
                    }

                    try
                    {
                        proxy.MyInfo(rez, (int)state);
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
