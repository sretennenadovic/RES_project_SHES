using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface ISolarPanel
    {
        [OperationContract]
        void listSolarPanels(Dictionary<string, double> panels, bool ready);
    }
}
