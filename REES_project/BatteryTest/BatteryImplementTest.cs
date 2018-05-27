using Battery;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryTest
{
    [TestFixture]
    public class BatteryImplementTest
    {

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public void DoWorkGoodParameter(int enumeration)
        {
            BatteryImplement batteryImplement = new BatteryImplement();

            batteryImplement.DoWork(enumeration);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-100)]
        [TestCase(4)]
        [TestCase(40)]
        [TestCase(400)]
        [TestCase("string")]
        [ExpectedException(typeof(ArgumentException))]
        public void DoWorkBadParameter(int enumeration)
        {
            BatteryImplement batteryImplement = new BatteryImplement();

            batteryImplement.DoWork(enumeration);
        }
    }
}
