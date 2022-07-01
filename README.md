# LiteNinja-EventBus
A Unity Event Bus for Unity.

What is an Event Bus? An event bus is a way to communicate between components of a game.

## Usage

### Events

    public class MyEvent : IEvent
    {
        public int Value { get; set; }
    }

### Event Bus
There are several ways to use LiteNinja-EventBus.

#### Global Event Bus
The global event bus is a static that can be accessed from anywhere in the game.
    
    GlobalEventBus.Subscribe<MyEvent>(OnMyEvent);
    
    GlobalEventBus.Publish(new MyEvent { Value = 1 });
    

#### Global Event Bus Generic

#### Simple Event Bus

#### ScriptableObject Event Bus

## Publishers

## Subscribers
It is responsibility of the subscriber to unsubscribe to the event bus.

