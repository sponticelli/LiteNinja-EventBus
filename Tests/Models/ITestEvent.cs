namespace LiteNinja.EventBus.Tests
{
    internal interface ITestEvent : IEvent
    {
    }
    
    internal interface ITestEvent<out TValue> : ITestEvent
    {
        public TValue Value { get; }
    }
}