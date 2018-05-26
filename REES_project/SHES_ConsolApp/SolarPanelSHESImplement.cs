using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    public class SolarPanelSHESImplement : ISolarPanelSHES
    {
        public void MyInfo(double power)
        {
            Console.WriteLine("Snaga svih panela je: {0}", power);
        }
    }
}
