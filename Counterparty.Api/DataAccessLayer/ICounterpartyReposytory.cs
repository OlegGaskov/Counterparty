using Counterparty.DataAccess.Models;
using System.Collections.Generic;

namespace Counterparty.DataAccess
{
    public interface ICounterpartyReposytory
    {
        IEnumerable<CounterpartyModel> FindAll();
        CounterpartyModel Find(int id);
        int Add(CounterpartyModel counterparty);
    }
}
