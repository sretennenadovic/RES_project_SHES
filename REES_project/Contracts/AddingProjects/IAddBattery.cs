using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.AddingProjects
{
    public interface IAddBattery
    {
        bool CheckValidation(string name, double capacity, double maxPower,Dictionary<string,Tuple<double,double[]>> dict);
    }
}
