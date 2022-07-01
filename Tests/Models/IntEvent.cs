namespace LiteNinja.EventBus.Tests
{
    internal class IntEvent : ITestEvent<int>
    {
        public int Value { get; }

        public IntEvent(int value)
        {
            Value = value;
        }
    }
}