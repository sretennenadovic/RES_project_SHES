using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.UtilityConnection
{
    [ServiceContract]
    public interface IUtility
    {
        [OperationContract]
        double CalculateMoney(double energy);
    }
}
