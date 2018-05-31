using Contracts.ConsumerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    public class ConsumerSHESImplement : IConsumerSHES
    {
        static double pom = 0;
        static int counter = 0;
        public void MyInfo(double consummation)
        {
            if(consummation < 0)
            {
                throw new ArgumentException("Invalid value of consummation!");
            }

            counter++;
            
            lock (Program.obj)
            {
                pom += consummation;

                if (counter == 10)
                {
                    pom = pom / 10;
                    
                    lock (Program.obj)
                    {
                        Program.consumerPom = consummation;
                    }
                    pom = 0; counter = 0;
                }
                
            }
        }
    }
}
