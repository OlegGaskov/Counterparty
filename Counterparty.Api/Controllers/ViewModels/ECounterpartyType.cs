using System.ComponentModel;


namespace Counterparty.DataAccess.Models
{
    public enum ECounterpartyType
    {
        [Description("Individual")]
        Individual = 0,

        [Description("Legal")]
        Legal = 1,
    }
}
