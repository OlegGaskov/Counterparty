using Counterparty.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Counterparty.BusinessLogic.Models.ServiceModels
{
    public interface ICounterpartyService
    {
        IEnumerable<CounterpartyModel> GetAll();
        CounterpartyModel Find(int id);
        int Add(CounterpartyModel account);
    }

}
