﻿using Battery;
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
        public static Dictionary<string, double[]> example1 = new Dictionary<string, double[]>();
        public static Dictionary<string, double[]> example2 = new Dictionary<string, double[]>();



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

        [Test]
        [TestCase(null, true)]
        [TestCase(null, false)]
        [ExpectedException(typeof(NullReferenceException))]
        public void ListBatteriesBadParameter1(Dictionary<string, double[]> batteries, bool ready)
        {
            BatteryImplement batteryImplement = new BatteryImplement();

            batteryImplement.ListBatteries(batteries, ready);
        }

        [Test]
        [TestCase(0, true)]
        [TestCase(0, false)]
        [ExpectedException(typeof(ArgumentException))]
        public void ListBatteriesBadParameter2(Dictionary<string, double[]> batteries, bool ready)
        {
            BatteryImplement batteryImplement = new BatteryImplement();

            batteryImplement.ListBatteries(batteries, ready);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        [ExpectedException(typeof(ArgumentException))]
        public void ListBatteriesBadParameter2(bool ready)
        {
            BatteryImplement batteryImplement = new BatteryImplement();

            batteryImplement.ListBatteries(example1, ready);
        }

        [Test]
        [TestCase(true)]
        public void ListBatteriesGoodParameter(bool ready)
        {
            BatteryImplement batteryImplement = new BatteryImplement();
            example2.Add("example" ,new double[] { 1, 2 });

            batteryImplement.ListBatteries(example2, ready);
        }
    }
}
