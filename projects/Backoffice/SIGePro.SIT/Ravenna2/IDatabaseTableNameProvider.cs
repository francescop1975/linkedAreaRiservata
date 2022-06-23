using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Sit.Ravenna2
{
    public interface IDatabaseTableNameProvider
    {
        string Ra012 { get; }
        string Ra147 { get; }
        string ParticelleCatastali { get; }
    }
}
