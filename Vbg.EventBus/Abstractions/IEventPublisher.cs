using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vbg.EventBus.Abstractions
{
    public interface IEventPublisher
    {
        void Load(EventBusModule module);
        void Publish<T>(T evento) where T : IEvent;
        void Add<TEvent, THandler>()
            where TEvent : IEvent
            where THandler : IEventSubscriber<TEvent>;
    }
}
