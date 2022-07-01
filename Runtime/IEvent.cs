namespace LiteNinja.EventBus
{
    public interface IEvent
    {
        
    }

    public interface IEvent<TData> : IEvent
    {
        TData Data { get; set; }
    }
}