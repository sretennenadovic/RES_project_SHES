using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    class Program
    {
        
        static void Main(string[] args)
        {
            StartProcesses();
            Console.ReadKey();
        }


        public static void StartProcesses()
        {
            // Pokrecemo Battery projekat
            Process p1 = new Process();
            p1.StartInfo.FileName = @"..\..\..\Battery\bin\Debug\Battery.exe";
            p1.Start();
            
            //Pokrecemo SolarPanel projekat
            Process p2 = new Process();
            p2.StartInfo.FileName = @"..\..\..\SolarPanel\bin\Debug\SolarPanel.exe";
            p2.Start();
        }
    }
}
