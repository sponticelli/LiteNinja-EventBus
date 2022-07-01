using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LiteNinja.EventBus
{
    /// <summary>
    /// An event bus based on ScriptableObject.
    /// </summary>
    /// <typeparam name="TEvent"></typeparam>
    public abstract class EventBusSO<TEvent> : ScriptableObject, IEventBus<TEvent> where TEvent : IEvent
    {
        protected EventBus<TEvent> _eventBus;

        public EventBus<TEvent> EventBus
        {
            get { return _eventBus ??= new EventBus<TEvent>(); }
        }

        protected virtual void OnEnable()
        {
            _eventBus ??= new EventBus<TEvent>();
        }

        protected void OnDisable()
        {
            _eventBus.UnsubscribeAll();
            _eventBus = null;
        }
        

        public void Subscribe<TEventType>(object subscriber, Action<TEventType> action) where TEventType : TEvent
        {
            _eventBus.Subscribe<TEventType>(subscriber, action);
        }


        public bool IsSubscribed<TEventType>(object subscriber) where TEventType : TEvent
        {
            return _eventBus.IsSubscribed<TEventType>(subscriber);
        }
        

        public IEnumerable<object> GetSubscribers<TEventType>() where TEventType : TEvent
        {
            return _eventBus.GetSubscribers<TEventType>();
        }

        public IEnumerable<Type> GetEventTypes(object subscriber)
        {
            return _eventBus.GetEventTypes(subscriber);
        }

        public IEnumerable<Type> GetEventTypes()
        {
            return _eventBus.GetEventTypes();
        }
        
        public bool Unsubscribe<TEventType>(object subscriber) where TEventType : TEvent
        {
            return _eventBus.Unsubscribe<TEventType>(subscriber);
        }

        public void Unsubscribe<TEventType>() where TEventType : TEvent
        {
            _eventBus.Unsubscribe<TEventType>();
        }
        

        public void UnsubscribeAll(object subscriber)
        {
            _eventBus.UnsubscribeAll(subscriber);
        }

        public void UnsubscribeAll()
        {
            _eventBus.UnsubscribeAll();
        }

        public void Publish(TEvent @event)
        {
            _eventBus.Publish(@event);
        }
    }
    
    [CreateAssetMenu(menuName = "LiteNinja/Event Bus/Create EventBusSO", fileName = "EventBusSO", order = 0)]
    public class EventBusSO : EventBusSO<IEvent>
    {
        public new EventBus<IEvent> EventBus => base.EventBus;
    }
}