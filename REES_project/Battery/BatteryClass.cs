using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battery
{
    public class BatteryClass
    {
        private string name;
        private double maxPower;
        private double capacity;

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public double MaxPower
        {
            get
            {
                return maxPower;
            }
            set
            {
                maxPower = value;
            }
        }

        public double Capacity
        {
            get
            {
                return capacity;
            }
            set
            {
                capacity = value;
            }
        }

        public BatteryClass(string n, double m, double c)
        {
            if (n == null || (m < 0 || m > 5) ||  (c < 0 || c > 5))
            {
                throw new ArgumentException("Argumenti ne smeju biti van domena");
            }

            if (n.Trim() == "")
            {
                throw new ArgumentException("Ime mora sadrzati karaktere");
            }

            this.Name = n;
            this.MaxPower = m;
            this.Capacity = c;
        }
    }
}
