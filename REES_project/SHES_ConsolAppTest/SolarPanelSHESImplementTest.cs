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
    public class SolarPanelSHESImplementTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(7)]
        [TestCase(9)]
        public void MyInfoGoodParameters(double power)
        {
            SolarPanelSHESImplement spsi = new SolarPanelSHESImplement();

            spsi.MyInfo(power);
        }

        [Test]
        [TestCase(0)]
        public void MyInfoBorderParameters(double power)
        {
            SolarPanelSHESImplement spsi = new SolarPanelSHESImplement();

            spsi.MyInfo(power);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-100)]
        [TestCase(-1000)]
        [ExpectedException(typeof(ArgumentException))]
        public void MyInfoBadParameters(double power)
        {
            SolarPanelSHESImplement spsi = new SolarPanelSHESImplement();

            spsi.MyInfo(power);
        }
    }
}
