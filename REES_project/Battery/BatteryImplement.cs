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
            Program.state = (Program.States)enumeration;
        }

        public void listBatteries(Dictionary<string, double[]> batteries, bool ready)
        {
            Program.batteries = batteries;
            Program.ready = ready;
        }
    }
}
