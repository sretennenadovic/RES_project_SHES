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
    class BatterySHESImplementTest
    {
        [Test]
        [TestCase(5,0)]
        [TestCase(5,1)]
        [TestCase(5,2)]
        public void MyInfoGoodParameters(double capacity,int state)
        {
            BatterySHESImplement bsi = new BatterySHESImplement();

            bsi.MyInfo(capacity, state);
        }

        [Test]
        [TestCase(5, -1)]
        [TestCase(5, 3)]
        [TestCase(5, -100)]
        [TestCase(5, 100)]
        [TestCase(5, -1000)]
        [TestCase(5, 1000)]
        [ExpectedException(typeof(ArgumentException))]
        public void MyInfoBadParameters(double capacity, int state)
        {
            BatterySHESImplement bsi = new BatterySHESImplement();

            bsi.MyInfo(capacity, state);
        }
    }
}
