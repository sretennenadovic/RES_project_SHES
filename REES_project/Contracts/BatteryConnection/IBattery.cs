using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface IBattery
    {
        [OperationContract]
        void DoWork(int enumeration);
        [OperationContract]
        void ListBatteries(Dictionary<string, Tuple<double,double[]>> batteries, bool ready);
    }
}
