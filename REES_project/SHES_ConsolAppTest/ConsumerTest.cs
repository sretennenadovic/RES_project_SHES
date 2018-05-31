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
    public class ConsumerTest
    {
        Dictionary<string, double> example2;
        Dictionary<string, double> example1 = new Dictionary<string, double>();


        [Test]
        [TestCase(null, 3)]
        [TestCase(null, 0.5)]
        [TestCase(null, 7)]
        [TestCase("", 7)]
        [TestCase("", 9)]
        [TestCase("", 0.5)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ConsumerCheckValidationNameIsNullorEmpty(string name, double consummation)
        {
            Consumer consumer = new Consumer();

            consumer.CheckValidation(name, consummation, example1);
        }



        [Test]
        [TestCase("sijalica", -1)]
        [TestCase("frizider", -0.5)]
        [TestCase("potrosac3", -100)]
        [TestCase("potrosac4", 100)]
        [TestCase("potrosac5", 41)]
        [TestCase("potrosac6", 43.5)]
        [ExpectedException(typeof(ArgumentException))]
        public void ConsumerCheckParameterValidationConsummationInvalid(string name, double consummation)
        {
            Consumer consumer = new Consumer();

            consumer.CheckValidation(name, consummation, example1);
        }



        [Test]
        [TestCase("potrosac1", 4)]
        [TestCase("potrosac2", 1)]
        [TestCase("potrosac3", 7.8)]
        [TestCase("potrosac4", 4.5)]
        [TestCase("potrosac5", 9)]
        [TestCase("potrosac6", 7.7)]
        [TestCase("potrosac7", 9.9)]
        public void ConsumerCheckValidationGoodParameters(string name, double consummation)
        {
            Consumer consumer = new Consumer();

            consumer.CheckValidation(name, consummation, example1);
        }

        [Test]
        [TestCase("a", 0)]
        [TestCase("asrdasdasd2", 0.0)]
        [TestCase("r", 6)]
        [TestCase("zzzzzzzzzz*zzzzzzzzzzzzzzzzz", 4)]
        [TestCase("3", 3)]
        [TestCase("#", 3.1)]
        [TestCase("!", 5)]

        public void ConsumerCheckValidationBorderNameParameter(string name, double consummation)
        {
            Consumer consumer = new Consumer();

            consumer.CheckValidation(name, consummation, example1);


        }

        [Test]
        [TestCase("potrosac1", 1)]
        [TestCase("potrosac2", 3)]
        [TestCase("potrosac2", 1)]
        [TestCase("potrosac1", 3)]
        [TestCase("potrosac2", 3)]
        [TestCase("potrosac1", 9)]
        [TestCase("potrosac1", 1)]
        [ExpectedException(typeof(ArgumentException))]

        public void ConsumerCheckValidationNameAlreadyExists(string name, double consummation)
        {
            Consumer consumer = new Consumer();
            example2 = new Dictionary<string, double>();
            example2.Add("potrosac1", 3);
            example2.Add("potrosac2", 5.9);

            consumer.CheckValidation(name, consummation, example2);


        }

        [Test]
        [TestCase("potrosac3", 1)]
        [TestCase("potrosac100", 3)]
        [TestCase("potrosac45", 1)]
        [TestCase("potrosac27", 3)]
        [TestCase("potrosac#", 3)]
        [TestCase("potrosac!", 9)]
        [TestCase("potrosac", 1)]

        public void ConsumerCheckValidationAllGoodParameters(string name, double consummation)
        {
            Consumer consumer = new Consumer();
            example2 = new Dictionary<string, double>();
            example2.Add("potrosac1", 3);
            example2.Add("potrosac2", 5.9);

            consumer.CheckValidation(name, consummation, example2);
        }
    }
}
