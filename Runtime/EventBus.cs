using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace LiteNinja.EventBus
{
    /// <summary>
    /// A simple event bus implementation.
    /// </summary>
    public class EventBus<TEvent> : IEventBus<TEvent> where TEvent : IEvent
    {
        private readonly Dictionary<Type, Dictionary<object, object>> _subscribers = new();

        public void Subscribe<TEventType>(object subscriber, Action<TEventType> action) where TEventType : TEvent
        {
            if (!_subscribers.ContainsKey(typeof(TEventType)))
            {
                _subscribers.Add(typeof(TEventType), new Dictionary<object, object>());
            }

            if (!_subscribers[typeof(TEventType)].ContainsKey(subscriber))
            {
                var tmp = action as object;
                _subscribers[typeof(TEventType)].Add(subscriber, action);
            }
            else
            {
                throw new ArgumentException($"{subscriber} is already subscribed to {typeof(TEventType)}.");
            }
        }

        public bool IsSubscribed<TEventType>(object subscriber) where TEventType : TEvent

        {
            return _subscribers.TryGetValue(typeof(TEventType), out var subscribers) &&
                   subscribers.ContainsKey(subscriber) &&
                   subscribers[subscriber] != null;
        }

        public IEnumerable<object> GetSubscribers<TEventType>() where TEventType : TEvent
        {
            return _subscribers.TryGetValue(typeof(TEventType), out var subscribers)
                ? subscribers.Keys
                : Enumerable.Empty<object>();
        }

        public IEnumerable<Type> GetEventTypes(object subscriber)
        {
            return _subscribers.Keys.Where(eventType => _subscribers[eventType].ContainsKey(subscriber));
        }


        public IEnumerable<Type> GetEventTypes()
        {
            return _subscribers.Keys;
        }

        public bool Unsubscribe<TEventType>(object subscriber) where TEventType : TEvent
        {
            if (!_subscribers.ContainsKey(typeof(TEventType)))
            {
                return false;
            }

            if (!_subscribers[typeof(TEventType)].ContainsKey(subscriber))
            {
                return false;
            }

            _subscribers[typeof(TEventType)].Remove(subscriber);
            return true;
        }

        public void Unsubscribe<TEventType>() where TEventType : TEvent
        {
            if (!_subscribers.TryGetValue(typeof(TEventType), out var subscribers))
            {
                return;
            }

            foreach (var subscriber in subscribers.Keys)
            {
                Unsubscribe<TEventType>(subscriber);
            }
        }

        public void UnsubscribeAll(object subscriber)
        {
            var eventTypes = _subscribers.Keys.Where(eventType => _subscribers[eventType].ContainsKey(subscriber));
            foreach (var eventType in eventTypes)
            {
                _subscribers[eventType].Remove(subscriber);
            }
        }

        public void UnsubscribeAll()
        {
            foreach (var eventType in _subscribers.Keys)
            {
                _subscribers[eventType].Clear();
            }
        }

        public void Publish(TEvent @event)
        {
            if (!_subscribers.TryGetValue(@event.GetType(), out var subscribers))
            {
                return;
            }

            var actions = subscribers.Keys.Select(subscriber => subscribers[subscriber] as Action<TEvent>);
            foreach (var action in actions)
            {
                action?.Invoke(@event);
            }
        }
        
    }
}