using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MessageBroker;
using FluentAssertions;

namespace MessageBrokerTests
{
    [TestClass]
    public class MessageBrokerTestCases
    {
        [TestMethod]
        public void Subscribe_StringType_ShouldSubscribe()
        {
            var broker = MessageBrokerImpl.Instance;
            var data = "Hello World";
            var sType = new StringTypeSubscriber(broker);
            sType.Subscribe();
            sType.OnConsumed += (sender, s) =>
            {
                s.Should().NotBe(data);
                s.Should().Be($"{sType.GetType().Name}{data}");
            };
        }

        [TestMethod]
        public void Subscribe_ComplexType_ShouldSubscribe()
        {
            var payload = new ComplexType { CreatedOn = DateTime.UtcNow, Id = 1, Name = Guid.NewGuid().ToString() };
            var broker = MessageBrokerImpl.Instance;
            var client = new ComplexTypeSubscriber(broker);
            client.Subscribe();
            client.OnConsumed += (sender, type) =>
            {
                type.Id.Should().NotBe(payload.Id);
                type.Name.Should().NotBe(payload.Name);
                Assert.IsTrue(type.CreatedOn < DateTime.UtcNow);
            };

            broker.Publish<ComplexType>(this, payload);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(10));
        }
    }
}
