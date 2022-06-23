using System;
using System.Collections;
using System.Collections.Generic;
using Init.SIGePro.Data;
using PersonalLib2.Data;
using PersonalLib2.Sql.Collections;

namespace Init.SIGePro.Manager
{

    public class ConcessioniTipiMgr : BaseManager
    {

        public ConcessioniTipiMgr(DataBase dataBase) : base(dataBase) { }

        public ConcessioniTipi GetById(String pTIPOCONCESSIONE)
        {
            ConcessioniTipi retVal = new ConcessioniTipi();
            retVal.TIPOCONCESSIONE = pTIPOCONCESSIONE;

            var mydc = db.GetClassList(retVal, true, false);
            if (mydc.Count != 0)
                return (mydc[0]) as ConcessioniTipi;

            return null;
        }


        public List<ConcessioniTipi> GetList(ConcessioniTipi p_class)
        {
            return this.GetList(p_class, null);
        }

        public List<ConcessioniTipi> GetList(ConcessioniTipi p_class, ConcessioniTipi p_cmpClass)
        {
            return db.GetClassList(p_class, p_cmpClass, false, false).ToList<ConcessioniTipi>();
        }

    }
}