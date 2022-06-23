using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vbg.EventBus.Abstractions;

namespace Vbg.EventBus.IOC
{
    public class EventBusIOCModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IEventPublisher>().ToMethod((ctxt) =>
            {
                return new EventPublisher(Kernel);

            }).InSingletonScope();
        }
    }
}
