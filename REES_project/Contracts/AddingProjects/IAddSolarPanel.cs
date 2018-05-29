using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.AddingProjects
{
    public interface IAddSolarPanel
    {
        bool CheckValidation(string name, double maxPower, Dictionary<string, double> dict);
    }
}
