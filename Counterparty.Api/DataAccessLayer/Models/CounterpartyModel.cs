
namespace Counterparty.DataAccess.Models
{
    public class CounterpartyModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Fullname { get; set; }
        public ECounterpartyType Type { get; set; }
        public string Inn { get; set; }
        public string Kpp { get; set; }
    }
}
