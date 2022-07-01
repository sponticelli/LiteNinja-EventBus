using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;


namespace LiteNinja.EventBus.Tests
{
    public class EventBusTesting
    {
        //Test to check if the event bus is instantiated
        [Test]
        public void EventBus_Instantiated()
        {
            var eventBus = new EventBus<StringEvent>();
            Assert.IsNotNull(eventBus);
        }

        //Test to check if the event bus is able to register an event
        [Test]
        public void Register_To_Multiple_Events()
        {
            var eventBus = new EventBus<ITestEvent>();
            var subscriber = new Subscriber(eventBus);

            const string stringValue = "Test";
            const int intValue = 1;

            subscriber.SubscribeToEvent<StringEvent>((@event) => { Assert.AreEqual(stringValue, @event.Value); });

            subscriber.SubscribeToEvent<IntEvent>((@event) => { Assert.AreEqual(intValue, @event.Value); });

            eventBus.Publish(new StringEvent(stringValue));
            eventBus.Publish(new IntEvent(intValue));
        }

        //Test to check if the event bus is able to register an event
        [Test]
        public void Register_To_Single_Event()
        {
            var eventBus = new EventBus<ITestEvent>();
            var subscriber = new Subscriber(eventBus);

            const string stringValue = "Test";

            subscriber.SubscribeToEvent<StringEvent>((@event) => { Assert.AreEqual(stringValue, @event.Value); });


            eventBus.Publish(new StringEvent(stringValue));
        }

        //Test multiple subscribers to the same event
        [Test]
        public void Multiple_Subscribers_To_Same_Event()
        {
            var eventBus = new EventBus<ITestEvent>();
            var subscriber = new Subscriber(eventBus);
            var subscriber2 = new Subscriber(eventBus);

            const string stringValue = "Test";

            subscriber.SubscribeToEvent<StringEvent>((@event) => { Assert.AreEqual(stringValue, @event.Value); });

            subscriber2.SubscribeToEvent<StringEvent>((@event) => { Assert.AreEqual(stringValue, @event.Value); });

            eventBus.Publish(new StringEvent(stringValue));
        }

        //Test to check if the event bus is able to unregister an event
        [Test]
        public void Unregister_From_Event()
        {
            var eventBus = new EventBus<ITestEvent>();
            var subscriber = new Subscriber(eventBus);

            const string stringValue = "Test";

            void Action(StringEvent @event)
            {
                Assert.AreEqual(stringValue, @event.Value);
            }

            subscriber.SubscribeToEvent((Action<StringEvent>)Action);

            Assert.IsTrue(subscriber.UnsubscribeFromEvent((Action<StringEvent>)Action), "Not able to unsubscribe from event");

            Assert.IsFalse(subscriber.IsSubscribedToEvent<StringEvent>(), "still subscribed to event");
        }
        
        //Test to check if the event bus is able to unregister to multiple events
        [Test]
        public void Unregister_From_Multiple_Events()
        {
            var eventBus = new EventBus<ITestEvent>();
            var subscriber = new Subscriber(eventBus);

            const string stringValue = "Test";

            void StringAction(StringEvent @event)
            {
                Assert.AreEqual(stringValue, @event.Value);
            }
            
            void IntAction(IntEvent @event)
            {
                Assert.AreEqual(1, @event.Value);
            }
            
            subscriber.SubscribeToEvent((Action<StringEvent>)StringAction);
            subscriber.SubscribeToEvent((Action<IntEvent>)IntAction);

            subscriber.UnsubscribeFromEvents();

            var count = eventBus.GetEventTypes(subscriber).Count();
            Assert.Zero(count);
        }
        
        //Test to check if multiple subscribers are able to unregister from the same event using UnsubscribeAll
        [Test]
        public void Unsubscribe_All_From_Event()
        {
            var eventBus = new EventBus<ITestEvent>();
            var subscriber = new Subscriber(eventBus);
            var subscriber2 = new Subscriber(eventBus);
            
            const string stringValue = "Test";
            
            void StringAction(StringEvent @event)
            {
                Assert.AreEqual(stringValue, @event.Value);
            }
            
            void IntAction(IntEvent @event)
            {
                Assert.AreEqual(1, @event.Value);
            }
            
            subscriber.SubscribeToEvent((Action<StringEvent>)StringAction);
            subscriber2.SubscribeToEvent((Action<StringEvent>)StringAction);
            subscriber.SubscribeToEvent((Action<IntEvent>)IntAction);
            subscriber2.SubscribeToEvent((Action<IntEvent>)IntAction);
            var count = eventBus.GetEventTypes().Count();
            eventBus.UnsubscribeAll();
            count = eventBus.GetEventTypes(subscriber).Count();
            Assert.Zero(count);
        }
        
        [Test]
        public void Removing_Subscriber_Doesnt_Affect_Other_Subscriber()
        {
            var eventBus = new EventBus<ITestEvent>();
            var subscriber = new Subscriber(eventBus);
            var subscriber2 = new Subscriber(eventBus);
            
            const string stringValue = "Test";
            
            void StringAction(StringEvent @event)
            {
                Assert.AreEqual(stringValue, @event.Value);
            }
            
            void IntAction(IntEvent @event)
            {
                Assert.AreEqual(1, @event.Value);
            }
            
            subscriber.SubscribeToEvent((Action<StringEvent>)StringAction);
            subscriber2.SubscribeToEvent((Action<StringEvent>)StringAction);
            subscriber.SubscribeToEvent((Action<IntEvent>)IntAction);
            subscriber2.SubscribeToEvent((Action<IntEvent>)IntAction);
            var count = eventBus.GetEventTypes().Count();
            subscriber.UnsubscribeFromEvents();
            count = eventBus.GetEventTypes().Count();
            Assert.AreEqual(count, 2);
            count = eventBus.GetEventTypes(subscriber2).Count();
            Assert.AreEqual(count, 2);
        }


    }
}