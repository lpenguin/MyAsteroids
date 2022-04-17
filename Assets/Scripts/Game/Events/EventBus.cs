using System;
using System.Collections.Generic;

namespace Game.Events
{
    public class EventBus
    {
        private readonly Dictionary<Type, HashSet<object>> _eventHandlersByType = new();
        
        public void PublishEvent<TEvent>(TEvent @event) where TEvent : class
        {
            Type type = @event.GetType();
            if (!_eventHandlersByType.TryGetValue(type, out var handlers))
                return;

            foreach (object handler in handlers)
            {
                if (handler is not Action<TEvent> typedHandler)
                    continue;

                typedHandler(@event);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : class
        {
            Type type = typeof(TEvent);
            if (!_eventHandlersByType.TryGetValue(type, out var handlers))
            {
                handlers = new HashSet<object>();
                _eventHandlersByType[type] = handlers;
            }

            handlers.Add(handler);
        }
        
        public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : class
        {
            Type type = typeof(TEvent);
            if (!_eventHandlersByType.TryGetValue(type, out var handlers))
            {
                return;
            }

            handlers.Remove(handler);
        }
    }
}