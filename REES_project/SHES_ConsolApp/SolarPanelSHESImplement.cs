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

        static int counter = 0;
        static double pom = 0;
        public void MyInfo(double power)
        {
            counter++;
            if(power < 0)
            {
                throw new ArgumentException("Invalid power value!");
            }
            
            lock (Program.obj)
            {
                pom += power;

                if (counter == 10)
                {
                    pom = pom / 10;

                    lock (Program.obj)
                    {
                        Program.panelPom = pom;
                        //Program.readyToCount = true;
                    }
                    pom = 0; counter = 0;
                }

            }
        }
    }
}
