using Contracts.ConsumerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class ConsumerImplement : IConsumer
    {
        public void ListConsumers(Dictionary<string, double> consumers, bool ready)
        {
            if (consumers == null)
            {
                throw new NullReferenceException("Dictionary can't be null!");
            }
            if (consumers.Count == 0)
            {
                throw new ArgumentException("Dictionary must have values!");
            }
            Program.consumers = consumers;
            Program.ready = ready;
        }
    }
}
