namespace LiteNinja.EventBus.Tests
{
    internal class StringEvent : ITestEvent<string>
    {
        public string Value { get; }

        public StringEvent(string value)
        {
            Value = value;
        }
    }
}