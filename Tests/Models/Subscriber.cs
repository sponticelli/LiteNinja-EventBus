using System;
using System.Linq;
using UnityEngine;

namespace LiteNinja.EventBus.Tests
{
    /// <summary>
    /// A class that subscribes to the event bus and checks if the event is fired
    /// </summary>
    internal class Subscriber
    {
        private readonly EventBus<ITestEvent> _eventBus;
        private readonly bool _debug;

        public Subscriber(EventBus<ITestEvent> eventBus, bool debug = false)
        {
            _eventBus = eventBus;
            _debug = debug;
        }

        public void SubscribeToEvent<T>(Action<T> action) where T : ITestEvent
        {
            _eventBus.Subscribe(this, OnEvent(action));
            if (!_debug) return;
            Debug.Log(this + " subscribed to " + typeof(T));
            Debug.Log(_eventBus.GetSubscribers<T>().Count());
        }


        public bool UnsubscribeFromEvent<T>(Action<T> action) where T : ITestEvent
        {
            var unsubscribed = _eventBus.Unsubscribe<T>(this);
            if (!_debug) return unsubscribed;
            if (unsubscribed) Debug.Log(this + " unsubscribed from " + typeof(T));
            else Debug.Log(this + " was not subscribed to " + typeof(T));
            Debug.Log(_eventBus.GetSubscribers<T>().Count());
            return unsubscribed;
        }

        public bool IsSubscribedToEvent<T>() where T : ITestEvent
        {
            return _eventBus.IsSubscribed<T>(this);
        }


        private static Action<T> OnEvent<T>(Action<T> action) where T : ITestEvent
        {
            return @event =>
            {
                var typedEvent = (T)@event;
                action(typedEvent);
            };
        }

        public void UnsubscribeFromEvents()
        {
            _eventBus.UnsubscribeAll(this);
        }
    }
}