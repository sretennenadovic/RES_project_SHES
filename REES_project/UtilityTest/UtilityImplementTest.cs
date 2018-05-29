using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace UtilityTest
{
    [TestFixture]
    public class UtilityImplementTest
    {
        [Test]
        [TestCase(5,ExpectedResult =50)]
        [TestCase(0,ExpectedResult =0)]
        [TestCase(1000,ExpectedResult =10000)]
        [TestCase(-5,ExpectedResult =-50)]
        [TestCase(-100,ExpectedResult =-1000)]
        [TestCase(-1000,ExpectedResult =-10000)]
        public double UtilityCalculateMoney(double energy)
        {
            UtilityImplement utilityImplement = new UtilityImplement();
            return utilityImplement.CalculateMoney(energy);
        }
    }
}
