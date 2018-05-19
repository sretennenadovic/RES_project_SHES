using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    class BatterySHESImplement : IBatterySHES
    {
        public void MyInfo(double capacity, int state)
        {
            Console.Write("Kapacitet baterija je: " + capacity + ", a trenutno su u stanju ");
            if(state == 0)
            {
                Console.WriteLine("\"punjenja\".");
            }else if (state == 1)
            {
                Console.WriteLine("\"praznjenja\".");
            }
            else
            {
                Console.WriteLine("\"ne radi\".");
            }
        }
    }
}
