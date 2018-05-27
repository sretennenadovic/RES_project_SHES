using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    [ServiceContract]
    public interface IBatterySHES
    {
        [OperationContract]
        void MyInfo(double capacity, int state);
    }
}
