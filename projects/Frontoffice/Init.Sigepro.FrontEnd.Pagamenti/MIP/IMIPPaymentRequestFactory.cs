﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.Pagamenti.MIP
{
    public interface IMIPPaymentRequestFactory
    {
        PaymentRequest Create(IniziaPagamentoRequest request);
    }
}
