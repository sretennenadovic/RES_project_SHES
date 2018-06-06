using Contracts.AddingProjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolApp
{
    public class Consumer : IAdding, IAddConsumer
    {
        public void Add()
        {
            Console.WriteLine("Insert number of consumers: ");
            int numberConsumers = Int32.Parse(Console.ReadLine());

            Console.WriteLine(@"**********ADDING CONSUMERS**********");
            for (int i = 0; i < numberConsumers; i++)
            {
                Console.WriteLine("Insert name of consumer: ");
                string name = Console.ReadLine();

                Console.WriteLine("Insert consummation: ");
                double consummation = double.Parse(Console.ReadLine());

                if (CheckValidation(name, consummation, Program.addConsumers))
                {
                    Program.addConsumers.Add(name, consummation);
                }

            }
            Console.WriteLine(@"*********CONSUMERS ADDED************
------------------------------------" + Environment.NewLine);
        }

        public bool CheckValidation(string name, double consummation, Dictionary<string, double> dict)
        {
            bool ret = true;

            if (string.IsNullOrEmpty(name))
            {
                ret = false;
                throw new ArgumentNullException("Name can't be null or empty!");
            }

            if (consummation < 0 || consummation >= 10)
            {
                ret = false;
                throw new ArgumentException("Consummation is invalid number!");
            }

            if (dict.Keys.Contains(name))
            {
                ret = false;
                throw new ArgumentException($"Consumer with name {name} alredy exists!");
            }

            return ret;
        }
    }
}
