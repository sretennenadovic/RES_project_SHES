using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battery
{
    class BatteryImplement : IBattery
    {
        public void DoWork(int enumeration)
        {
            Program.stanje = (Program.Stanja)enumeration;
        }

        public void listBatteries(Dictionary<string, double[]> baterije, bool spreman)
        {
            Program.baterija = baterije;
            Program.spremanSam = spreman;
        }
    }
}
