using MessageBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBrokerTests
{
    public class StringTypeSubscriber
    {
        private IMessageBroker _broker;
        public event EventHandler<string> OnConsumed;
        public StringTypeSubscriber(IMessageBroker broker)
        {
            _broker = broker;
        }

        public void Subscribe()
        {
            _broker.Subscribe<string>(HandleStringType);
        }

        public void HandleStringType(MessagePayload<string> data)
        {
            var consumed = $"{GetType().Name}{data.What}";
            OnConsumed?.Invoke(data.Who, consumed);
        }
    }
}
