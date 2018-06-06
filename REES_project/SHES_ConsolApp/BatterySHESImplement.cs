using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    public class BatterySHESImplement : IBatterySHES
    {
        static int counter = 0;
        static double pom = 0;
        public void MyInfo(double capacity, int state)
        {
            counter++;
            
            if(state == 0)
            {
                Program.state = Program.States.PUNJENJE;
            }
            else if (state == 1)
            {
                Program.state = Program.States.PRAZNJENJE;
            }
            else if (state == 2)
            {
                Program.state = Program.States.ISKLJUCENA;
            }
            else
            {
                throw new ArgumentException("Invalid state!");
            }
            
            pom += capacity;

            if (counter == 10)
            {
                pom = pom / 10;
                
                lock (Program.obj)
                {
                    Program.batteryPom = pom;
                    Program.readyToCount = true;
                }
                pom = 0;
                counter = 0;
            }
        }
    }
}
