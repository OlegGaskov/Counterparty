using Counterparty.Api.ViewModels;
using Counterparty.BusinessLogic.Models.ServiceModels;
using Counterparty.DataAccess;
using Counterparty.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dadata.SmallApiClient.Models;
using Dadata.SmallApiClient.Models.ResponceModels;
using System.Net.Http;

namespace Counterparty.BusinessLogic.Services
{
    public class CounterpartyService : ICounterpartyService
    {
        private readonly ICounterpartyReposytory _counterpartyReposytory;
        public CounterpartyService(ICounterpartyReposytory accountReposytory)
        {
            _counterpartyReposytory = accountReposytory;
        }
        public int Add(CounterpartyModel account)
        {
            return _counterpartyReposytory.Add(account);
        }
        public CounterpartyModel Find(int id)
        {
            return _counterpartyReposytory.Find(id);
        }
        public IEnumerable<CounterpartyModel> GetAll()
        {
            return _counterpartyReposytory.FindAll();
        }
    }
}
