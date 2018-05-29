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
    public class SolarPanelTest
    {
        public static Dictionary<string, double> example = new Dictionary<string, double>();
        Dictionary<string, double> example2;

        [Test]
        [TestCase(null,2)]
        [TestCase(null,9)]
        [TestCase("",9)]
        [TestCase("",2)]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidationCheckNameIsNullOrEmpty(string name,double maxPower)
        {
            SolarPanel solarPanel = new SolarPanel();

            solarPanel.CheckValidation(name,maxPower,example);
        }

        [Test]
        [TestCase("name1", -1)]
        [TestCase("name2", -100)]
        [TestCase("name3", 100)]
        [TestCase("name4", 1000)]
        [ExpectedException(typeof(ArgumentException))]
        public void SolarPanelCheckValidationMaxPowerInvalid(string name, double maxPower)
        {
            SolarPanel solarPanel = new SolarPanel();

            solarPanel.CheckValidation(name,maxPower, example);
        }

        [Test]
        [TestCase("name1", 1)]
        [TestCase("name1", 5)]
        [TestCase("name1", 7)]
        [TestCase("name1", 8)]
        [TestCase("name2", 1)]
        [TestCase("name2", 6)]
        [TestCase("name2", 3)]
        [TestCase("name2", 9)]
        [ExpectedException(typeof(ArgumentException))]
        public void SolarPanelCheckValidationNameAlreadyExists(string name, double maxPower)
        {
            SolarPanel solarPanel = new SolarPanel();
            example2 = new Dictionary<string, double>();
            example2.Add("name1", 5);
            example2.Add("name2", 5);

            solarPanel.CheckValidation(name, maxPower, example2);
        }

        [Test]
        [TestCase("name1", 1)]
        [TestCase("name4", 5)]
        [TestCase("name5", 7)]
        [TestCase("name15", 8)]
        [TestCase("z#z#z", 8)]
        public void SolarPanelCheckValidationGoodParameters(string name, double maxPower)
        {
            SolarPanel solarPanel = new SolarPanel();
            example2 = new Dictionary<string, double>();
            example2.Add("name2", 5);
            example2.Add("name3", 9);

            solarPanel.CheckValidation(name, maxPower, example2);
        }
    }
}
