namespace MessageBroker
{
    using System;
    public interface IMessageBroker : IDisposable
    {
        void Publish<T>(object source, T message);
        void Subscribe<T>(Action<MessagePayload<T>> subscription);
        void Unsubscribe<T>(Action<MessagePayload<T>> subscription);
        bool IsPersistent { get; }
        bool Persist();
    }
}
