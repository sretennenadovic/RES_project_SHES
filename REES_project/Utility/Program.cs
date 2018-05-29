using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    class Program
    {
        #region Fields
        public static ServiceHost sh = new ServiceHost(typeof(UtilityImplement));
        public static ConnectionClass connectionClass = new ConnectionClass();
        #endregion Fields
        static void Main(string[] args)
        {
            connectionClass.OpenConnection();

            Console.ReadLine();
        }
    }
}
