using Contracts.AddingProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    public class Battery : IAdding, IAddBattery
    {
        public Battery()
        {

        }
        public void Add()
        {
            Console.WriteLine("Insert number of batteries: ");
            int numberButteries = Int32.Parse(Console.ReadLine());

            Console.WriteLine(@"*********ADDING BATTERIES***********");
            for (int i = 0; i < numberButteries; i++)
            {
                Console.WriteLine("Insert name of battery: ");
                string name = Console.ReadLine();

                Console.WriteLine("Insert capacity: ");
                double capacity = double.Parse(Console.ReadLine());

                Console.WriteLine("Insert max power of battery: ");
                double maxPower = double.Parse(Console.ReadLine());
               
                if (CheckValidation(name, capacity, maxPower,Program.addBatteries))
                {
                    Program.addBatteries.Add(name,new Tuple<double, double[]>(capacity, new double[] { capacity, maxPower }));
                }
                
            }
            Console.WriteLine(@"**********BATTERIES ADDED***********
------------------------------------" + Environment.NewLine);
        }

        public bool CheckValidation(string name, double capacity, double maxPower,Dictionary<string,Tuple<double,double[]>> dict)
        {
            bool ret = true;

            if (string.IsNullOrEmpty(name))
            {
                ret = false;
                throw new ArgumentNullException("Name can't be null or empty!");
            }

            if (capacity <= 0 || capacity >= 10)
            {
                ret = false;
                throw new ArgumentException("Capacity is invalid number!");
            }

            if (maxPower <= 0 || maxPower >= 10)
            {
                ret = false;
                throw new ArgumentException("Max power is invalid number!");
            }

            if (dict.Keys.Contains(name))
            {
                ret = false;
                throw new ArgumentException($"Battery with name {name} alredy exists!");
            }


            return ret;
        }
    }
}
