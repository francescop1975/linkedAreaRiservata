namespace Init.Sigepro.FrontEnd.Pagamenti.NODOPAGAMENTI
{
    public interface IContoDTO
    {
        int? AnnoAccertamento { get;  }
        string CodiceConto { get;  }
        string CodiceRiferimentoCausale { get;  }
        string CodiceSottoConto { get;  }
        string Conto { get;  }
        int Id { get;  }
        int? Iva { get;  }
        string NumeroAccertamento { get;  }
        string NumeroSottoAccertamento { get;  }
    }
}