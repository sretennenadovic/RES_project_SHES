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

        public void listBatteries(Dictionary<string, double[]> batteries, bool ready)
        {
            Program.batteries = batteries;
            Program.ready = ready;
        }
    }
}
