using Contracts.UtilityConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    class ConnectionClass
    {
        public void OpenConnection()
        {
            //dizemo servis na utility
            Program.sh.AddServiceEndpoint(typeof(IUtility), new NetTcpBinding(), new Uri("net.tcp://localhost:10050/IUtility"));
            Program.sh.Open();
        }

    }
}
