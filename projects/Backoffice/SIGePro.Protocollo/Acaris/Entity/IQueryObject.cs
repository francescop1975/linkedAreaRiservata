﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Protocollo.Acaris.Entity
{
    public interface IQueryObject : IIDAcaris
    {
        int Id { get; }
        
    }
}
