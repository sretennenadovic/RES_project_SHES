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
        [TestCase(null, true)]
        [TestCase(null, false)]
        [ExpectedException(typeof(NullReferenceException))]
        public void ListSolarPanelBadParameters1(Dictionary<string, double> solarPanels, bool ready)
        {
            SolarPanelImplement solarPanelImplement = new SolarPanelImplement();

            solarPanelImplement.listSolarPanels(solarPanels, ready);
        }

        [Test]
        [TestCase(0, true)]
        [TestCase(0, false)]
        [ExpectedException(typeof(ArgumentException))]
        public void ListBatteriesBadParameters2(Dictionary<string, double> solarPanels, bool ready)
        {
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
