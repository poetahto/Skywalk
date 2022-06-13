using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace CardGame.Data
{
    [PublicAPI]
    public class EventBus
    {
        private Dictionary<Type, List<object>> _handlers;

        public EventBus()
        {
            _handlers = new Dictionary<Type, List<object>>();
        }
        
        /// <summary>
        /// Listen for an event of type TData to be published to the bus, and respond with a callback.
        /// </summary>
        /// <param name="handler">The callback to execute when an event occurs.</param>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        
        public IDisposable Subscribe<TData>(Action<TData> handler)
        {
            var dataType = typeof(TData);
            
            if (!_handlers.ContainsKey(dataType))
                _handlers.Add(dataType, new List<object>());
                
            _handlers[dataType].Add(handler);

            return new UnsubscribeDisposable<TData>(_handlers, handler);
        }

        public void Publish<TData>(TData data)
        {
            var dataType = typeof(TData);
            
            if (_handlers.ContainsKey(dataType))
            {
                foreach (Action<TData> action in _handlers[dataType])
                    action.Invoke(data);
            }
        }
        
        private class UnsubscribeDisposable<TData> : IDisposable
        {
            private Dictionary<Type, List<object>> _handlers;
            private object _value;
            
            public UnsubscribeDisposable(
                Dictionary<Type, List<object>> handlers, 
                object value)
            {
                _handlers = handlers;
                _value = value;
            }
            
            public void Dispose()
            {
                var dataType = typeof(TData);
                _handlers[dataType].Remove(_value);
            }
        }
    }
}