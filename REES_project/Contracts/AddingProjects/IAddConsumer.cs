using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.AddingProjects
{
    public interface IAddConsumer
    {
        bool CheckValidation(string name, double consummation, Dictionary<string, double> dict);
    }
}
