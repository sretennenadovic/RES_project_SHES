using Contracts;
using Contracts.AddingProjects;
using Moq;
using Moq.Language.Flow;
using NUnit.Framework;
using SHES_ConsolApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHES_ConsolAppTest
{
    [TestFixture]
    public class BatteryTest
    {
        Dictionary<string, Tuple<double,double[]>> example2;
        Dictionary<string, Tuple<double, double[]>> example1 = new Dictionary<string, Tuple<double, double[]>>();
        

        [Test]
        [TestCase(null,3,5)]
        [TestCase(null,7,1)]
        [TestCase("",7,5)]
        [TestCase("",9,3)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void BatteryCheckValidationNameIsNullorEmpty(string name,double capacity,double maxPower)
        {
            Battery battery = new Battery();

            battery.CheckValidation(name, capacity, maxPower,example1);
        }

        [Test]
        [TestCase("duracel", -1, 5)]
        [TestCase("varta", 0, 1)]
        [TestCase("duracel", 11, 5)]
        [TestCase("varta", 100, 3)]
        [ExpectedException(typeof(ArgumentException))]
        public void BatteryCheckValidationCapacityInvalid(string name, double capacity, double maxPower)
        {
            Battery battery = new Battery();
 
            battery.CheckValidation(name, capacity, maxPower,example1);
        }

        [Test]
        [TestCase("duracel", 4, -1)]
        [TestCase("varta", 0, -100)]
        [TestCase("duracel", 4, 100)]
        [TestCase("varta", 2, 11)]
        [ExpectedException(typeof(ArgumentException))]
        public void BatteryCheckValidationMaxPowerInvalid(string name, double capacity, double maxPower)
        {
            Battery battery = new Battery();

            battery.CheckValidation(name, capacity, maxPower,example1);
        }



        [Test]
        [TestCase("duracel", 4, 5)]
        [TestCase("varta", 1, 9)]
        [TestCase("duracel", 4, 6)]
        [TestCase("varta", 4, 4)]
        [TestCase("duracel", 9, 9)]
        [TestCase("varta", 1, 1)]
        [TestCase("duracel", 9, 1)]
        public void BatteryCheckValidationGoodParameters(string name, double capacity, double maxPower)
        {
            Battery battery = new Battery();

            battery.CheckValidation(name, capacity, maxPower,example1);
        }

        [Test]
        [TestCase("a", 1, 5)]
        [TestCase("asrdasdasd2", 3, 3)]
        [TestCase("r", 1, 7)]
        [TestCase("zzzzzzzzzzzzzzzzzzzzzzzzzzz", 3, 4)]
        [TestCase("3", 3, 4)]

        public void BatteryCheckValidationBorderNameParameter(string name, double capacity, double maxPower)
        {
            Battery battery = new Battery();

            battery.CheckValidation(name, capacity, maxPower, example1);
           
            
        }

        [Test]
        [TestCase("duracel", 1, 5)]
        [TestCase("duracel", 3, 3)]
        [TestCase("duracel", 1, 7)]
        [TestCase("duracel", 3, 4)]
        [TestCase("varta", 3, 4)]
        [TestCase("varta", 9, 9)]
        [TestCase("varta", 1, 1)]
        [ExpectedException(typeof(ArgumentException))]

        public void BatteryCheckValidationNameAlreadyExists(string name, double capacity, double maxPower)
        {
            Battery battery = new Battery();
            example2 = new Dictionary<string, Tuple<double,double[]>>();
            example2.Add("duracel", new Tuple<double, double[]>(3,new double[] { 3, 4 }));
            example2.Add("varta", new Tuple<double, double[]>(3, new double[] { 3, 4 }));

            battery.CheckValidation(name, capacity, maxPower, example2);


        }

        [Test]
        [TestCase("baterija1", 1, 5)]
        [TestCase("baterija2", 3, 3)]
        [TestCase("baterija1", 1, 7)]
        [TestCase("baterija3", 3, 4)]
        [TestCase("baterija4", 3, 4)]
        [TestCase("baterija5", 9, 9)]
        [TestCase("baterija1000", 1, 1)]

        public void BatteryCheckValidationAllGoodParameters(string name, double capacity, double maxPower)
        {
            Battery battery = new Battery();
            example2 = new Dictionary<string, Tuple<double,double[]>>();
            example2.Add("duracel", new Tuple<double, double[]>(3, new double[] { 3, 4 }));
            example2.Add("varta", new Tuple<double, double[]>(3, new double[] { 3, 4 }));

            battery.CheckValidation(name, capacity, maxPower, example2);


        }
    }
}
