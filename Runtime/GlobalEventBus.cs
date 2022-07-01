using System;
using System.Collections.Generic;

namespace LiteNinja.EventBus
{
    /// <summary>
    /// GlobalEventBus that can limit the scope of the event to a specific type.
    /// </summary>
    public abstract class GlobalEventBus<TEvent> where TEvent : IEvent
    {
        private static readonly IEventBus<TEvent> _eventBus = new EventBus<TEvent>();

        public static void Subscribe<TEventType>(object subscriber, Action<TEventType> action) where TEventType : TEvent
        {
            _eventBus.Subscribe<TEventType>(subscriber, action);
        }

        public static bool IsSubscribed<TEventType>(Type eventType, object subscriber) where TEventType : TEvent
        {
            return _eventBus.IsSubscribed<TEventType>(subscriber);
        }

        public static IEnumerable<object> GetSubscribers<TEventType>(Type eventType) where TEventType : TEvent
        {
            return _eventBus.GetSubscribers<TEventType>();
        }


        public static IEnumerable<Type> GetEvents(object subscriber)
        {
            return _eventBus.GetEventTypes(subscriber);
        }

        public static void Unsubscribe<TEventType>(object subscriber)
            where TEventType : TEvent
        {
            _eventBus.Unsubscribe<TEventType>(subscriber);
        }

        public static void UnsubscribeAll(object subscriber)
        {
            _eventBus.UnsubscribeAll(subscriber);
        }

        public static void UnsubscribeAll()
        {
            _eventBus.UnsubscribeAll();
        }

        public static void Publish(TEvent @event)
        {
            _eventBus.Publish(@event);
        }
    }

    /// <summary>
    /// A static GlobalEventBus that can be used to publish and subscribe to any IEvent
    /// </summary>
    public static class GlobalEventBus
    {
        private static readonly IEventBus<IEvent> _eventBus = new EventBus<IEvent>();

        public static void Subscribe<TEventType>(object subscriber, Action<TEventType> action) where TEventType : IEvent
        {
            _eventBus.Subscribe<TEventType>(subscriber, action);
        }

        public static bool IsSubscribed<TEventType>(object subscriber) where TEventType : IEvent
        {
            return _eventBus.IsSubscribed<TEventType>(subscriber);
        }

        public static IEnumerable<object> GetSubscribers<TEventType>() where TEventType : IEvent
        {
            return _eventBus.GetSubscribers<TEventType>();
        }

        public static IEnumerable<Type> GetEvents(object subscriber)
        {
            return _eventBus.GetEventTypes(subscriber);
        }

        public static bool Unsubscribe<TEventType>(object subscriber) where TEventType : IEvent
        {
            return _eventBus.Unsubscribe<TEventType>(subscriber);
        }
        
        public static void Unsubscribe<TEventType>() where TEventType : IEvent
        {
            _eventBus.Unsubscribe<TEventType>();
        }


        public static void UnsubscribeAll(object subscriber)
        {
            _eventBus.UnsubscribeAll(subscriber);
        }
        
        public static void UnsubscribeAll() {
            _eventBus.UnsubscribeAll();
        }

        public static void Publish(IEvent @event)
        {
            _eventBus.Publish(@event);
        }
    }
}