using Contracts.AddingProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    public class SolarPanel : IAdding,IAddSolarPanel
    {
        public void Add()
        {
            Console.WriteLine("Insert number of solar panels: ");
            int numberPanels = Int32.Parse(Console.ReadLine());

            Console.WriteLine(@"------------------------------------
************************************");
            for (int i = 0; i < numberPanels; i++)
            {
                Console.WriteLine("Insert name of panel: ");
                string name = Console.ReadLine();

                Console.WriteLine("Insert max power of solar panel: ");
                double maxPower = double.Parse(Console.ReadLine());

                if (CheckValidation(name, maxPower, Program.addSolarPanels))
                {
                    Program.addSolarPanels.Add(name, maxPower);
                }

            }
            Console.WriteLine(@"************************************
------------------------------------");
        }

        public bool CheckValidation(string name, double maxPower, Dictionary<string, double> dict)
        {
            bool ret = true;

            if (String.IsNullOrEmpty(name))
            {
                ret = false;
                throw new ArgumentException("Name can't be null or empty!");
            }
            if (maxPower < 0 || maxPower > 10)
            {
                ret = false;
                throw new ArgumentException("Max power is invalid number!");
            }
            if (dict.Keys.Contains(name))
            {
                ret = false;
                throw new ArgumentException($"Solar panel with name {name} alredy exists!");
            }

            return ret;
        }
    }
}
