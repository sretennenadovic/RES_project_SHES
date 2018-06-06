using NUnit.Framework;
using SolarPanel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarPanelTest
{
    [TestFixture]
    public class SolarPanelImplementTest
    {
        public static Dictionary<string, double> example = new Dictionary<string, double>();

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        [ExpectedException(typeof(ArgumentException))]
        public void ListBatteriesBadParameters2( bool ready)
        {
            Dictionary<string, double> solarPanels = new Dictionary<string, double>();
            SolarPanelImplement solarPanelImplement = new SolarPanelImplement();

            solarPanelImplement.listSolarPanels(solarPanels, ready);
        }

        [Test]
        [TestCase(true)]
        public void ListBatteriesGoodParameter1(bool ready)
        {
            SolarPanelImplement solarPanelImplement = new SolarPanelImplement();

            example.Add("example", 1);

            solarPanelImplement.listSolarPanels(example, ready);
        }
    }
}
