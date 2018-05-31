using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.ConsumerConnection
{
    [ServiceContract]
    public interface IConsumer
    {
        [OperationContract]
        void ListConsumers(Dictionary<string, double> consumers, bool ready);
    }
}
