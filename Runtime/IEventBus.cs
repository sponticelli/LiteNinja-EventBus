using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace LiteNinja.EventBus
{
    public interface IEventBus<in TEvent> where TEvent : IEvent
    {
        

        /// <summary>
        /// Subscribe to an event with a callback.
        /// </summary
        void Subscribe<TEventType>(object subscriber, Action<TEventType> action) where TEventType : TEvent;

        
        /// <summary>
        /// Checks if the subscriber is subscribed to the event type.
        /// </summary>
        bool IsSubscribed<TEventType>(object subscriber) where TEventType : TEvent;
        

        /// <summary>
        /// Get all subscribers of the event type.
        /// </summary>
        IEnumerable<object> GetSubscribers<TEventType>() where TEventType : TEvent;

        /// <summary>
        /// Get all eventTypes that are subscribed to.
        /// </summary>
        IEnumerable<Type> GetEventTypes(object subscriber);

        /// <summary>
        /// Unsubscribe from an event.
        /// </summary>
        bool Unsubscribe<TEventType>(object subscriber) where TEventType : TEvent;


        /// <summary>
        /// Unsubscribe every subscriber from an event.
        /// </summary>
        void Unsubscribe<TEventType>() where TEventType : TEvent;

        /// <summary>
        /// Unsubscribe a subscriber from all events.
        /// </summary>
        void UnsubscribeAll(object subscriber);

        /// <summary>
        /// Unsubscribe all subscribers from all events.
        /// </summary>
        void UnsubscribeAll();

        /// <summary>
        /// Publish an event. Every subscriber will be notified.
        /// </summary>
        void Publish(TEvent @event);
    }
}