using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.IOC
{
    public static class KernelContainer
    {
        public static IKernel Kernel { get; set; }
        public static T GetService<T>()
        {
            if (Kernel == null)
            {
                return default(T);
            }

            return (T)Kernel.GetService(typeof(T));
        }
    }
}
