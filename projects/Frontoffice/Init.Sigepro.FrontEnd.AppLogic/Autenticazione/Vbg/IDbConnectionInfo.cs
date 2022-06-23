using PersonalLib2.Data;

namespace Init.Sigepro.FrontEnd.AppLogic.Autenticazione.Vbg
{
    internal interface IDbConnectionInfo
    {
        IDatabase CreateDatabase(string token);
    }
}