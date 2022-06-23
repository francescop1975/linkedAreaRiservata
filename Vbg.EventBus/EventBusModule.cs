using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vbg.EventBus.Abstractions;

namespace Vbg.EventBus
{
    public abstract class EventBusModule
    {
        protected IEventPublisher EventPublisher;

        internal void RegisterInternal(IEventPublisher eventPublisher)
        {
            this.EventPublisher = eventPublisher;

            this.Load();
        }

        public abstract void Load();
    }
}
