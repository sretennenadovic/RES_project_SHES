using Consumer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerTest
{
    [TestFixture]
    public class ConsumerImplementTest
    {
        public static Dictionary<string, double> example1;

        [Test]
        [TestCase(null, true)]
        [TestCase(null, false)]
        [ExpectedException(typeof(NullReferenceException))]
        public void ListConsumersNullDictionary(Dictionary<string, double> consumers, bool ready)
        {
            ConsumerImplement consumerImplement = new ConsumerImplement();

            consumerImplement.ListConsumers(consumers, ready);
        }

        [Test]
        [TestCase(0, true)]
        [TestCase(0, false)]
        [ExpectedException(typeof(ArgumentException))]
        public void ListConsumersEmptyDictionary(Dictionary<string, double> consumers, bool ready)
        {
            ConsumerImplement consumerImplement = new ConsumerImplement();

            consumerImplement.ListConsumers(consumers, ready);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void ListConsumersGoodDictionary(bool ready)
        {
            ConsumerImplement consumerImplement = new ConsumerImplement();
            example1 = new Dictionary<string, double>();
            example1.Add("example0", 1);
            example1.Add("example1", 2);
            example1.Add("example2", 3);

            consumerImplement.ListConsumers(example1, ready);
        }
    }
}
