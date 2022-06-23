using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vbg.EventBus.Abstractions;

namespace Vbg.EventBus
{
    public class EventPublisher : IEventPublisher
    {
        Dictionary<Type, List<Type>> _subscribers = new Dictionary<Type, List<Type>>();
        private readonly IKernel _kernel;

        public EventPublisher(IKernel kernel)
        {
            _kernel = kernel;
        }


        // Vedi https://github.com/dotnet-architecture/eShopOnContainers/blob/dev/src/BuildingBlocks/EventBus/EventBus/Abstractions/IEventBus.cs
        public void Add<TEvent, THandler>()
            where TEvent : IEvent
            where THandler : IEventSubscriber<TEvent>
        {
            var tipoEvento = typeof(TEvent);
            var tipoGestore = typeof(THandler);
            var subscribersPerEvento = new List<Type>();

            if (!this._subscribers.TryGetValue(tipoEvento, out subscribersPerEvento))
            {
                subscribersPerEvento = new List<Type>();

                this._subscribers[tipoEvento] = subscribersPerEvento;
            }

            subscribersPerEvento.Add(tipoGestore);
        }

        public void Publish<T>(T evento) where T : IEvent
        {
            var type = typeof(T);

            if (this._subscribers.ContainsKey(type))
            {
                this._subscribers[type].ForEach(x =>
                {
                    var gestore = this._kernel.Get(x);

                    var methodInfo = x.GetMethod("OnEvento", new[] { typeof(T) });
                    methodInfo.Invoke(gestore, new object[] { evento });
                });
            }
        }

        public void Load(EventBusModule module)
        {
            module.RegisterInternal(this);
        }
    }
}
