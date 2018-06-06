using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battery
{
    public class BatteryImplement : IBattery
    {
        public void DoWork(int enumeration)
        {
            if(enumeration != 0 && enumeration!=1 && enumeration != 2)
            {
                throw new ArgumentException("Invalid command!");
            }
            Program.state = (Program.States)enumeration;
        }

        public void ListBatteries(Dictionary<string, Tuple<double,double[]>> batteries, bool ready)
        {
            if (batteries == null)
            {
                throw new NullReferenceException("Dictionary can't be null!");
            }
            if(batteries.Count == 0)
            {
                throw new ArgumentException("Dictionary must have values!");
            }
            Program.batteries = batteries;
            Program.ready = ready;

            int i = 0;

            //pravimo storage(niz) koji sadrzi za svaku bateriju koja je energija koju ona salje
            Program.storage = new double[batteries.Count];

            foreach (var item in batteries)
            {
                Program.storage[i++] = (item.Value.Item2[1]/item.Value.Item2[0]);
            }
        }
    }
}
