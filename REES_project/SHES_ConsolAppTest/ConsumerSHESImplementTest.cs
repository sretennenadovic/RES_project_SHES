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
    public class ConsumerSHESImplementTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(2.3)]
        [TestCase(5)]
        [TestCase(7.8)]
        [TestCase(9)]
        [TestCase(9.1)]
        public void MyInfoGoodParameters(double consummation)
        {
            ConsumerSHESImplement spsi = new ConsumerSHESImplement();

            spsi.MyInfo(consummation);
        }

        [Test]
        [TestCase(0)]
        [TestCase(0.0)]
        [TestCase(9.9)]
        public void MyInfoBorderParameters(double consummation)
        {
            ConsumerSHESImplement spsi = new ConsumerSHESImplement();

            spsi.MyInfo(consummation);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-100.1)]
        [TestCase(-1000)]
        [ExpectedException(typeof(ArgumentException))]
        public void MyInfoBadParameters(double consummation)
        {
            ConsumerSHESImplement spsi = new ConsumerSHESImplement();

            spsi.MyInfo(consummation);
        }
    }
}
