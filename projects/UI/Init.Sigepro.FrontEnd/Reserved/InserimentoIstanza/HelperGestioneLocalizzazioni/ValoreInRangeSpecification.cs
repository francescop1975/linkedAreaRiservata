﻿using Init.Sigepro.FrontEnd.Infrastructure;
using Init.Sigepro.FrontEnd.Reserved.InserimentoIstanza.HelperGestioneLocalizzazioni;

public class ValoreInRangeSpecification : ISpecification<IValoreinRangeVerificabile>
{
    public bool IsSatisfiedBy(IValoreinRangeVerificabile item)
    {
        return item.VerificaValoreInRange();
    }
}