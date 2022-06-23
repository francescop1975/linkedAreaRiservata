using PersonalLib2.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.SIGePro.Manager.Utils
{
    public interface IDatabaseCreator
    {
        DataBase Create();
    }
}
