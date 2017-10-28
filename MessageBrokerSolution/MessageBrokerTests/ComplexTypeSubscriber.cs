using MessageBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBrokerTests
{
    public class ComplexTypeSubscriber
    {
        private readonly IMessageBroker _broker;
        public event EventHandler<ComplexType> OnConsumed;
        public ComplexTypeSubscriber(IMessageBroker broker) { _broker = broker; }
        public void Subscribe()
        {
            _broker.Subscribe<ComplexType>(HandleComplexType);
        }

        internal void HandleComplexType(MessagePayload<ComplexType> data)
        {
            var message = data.What;
            message.Id = ++data.What.Id;
            message.Name = Guid.NewGuid().ToString("N");
            message.CreatedOn = DateTime.UtcNow;
            OnConsumed?.Invoke(data.Who, message);
        }
    }
}
