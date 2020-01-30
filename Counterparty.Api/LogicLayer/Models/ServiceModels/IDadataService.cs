using Counterparty.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counterparty.Api.LogicLayer.Models.ServiceModels
{
    public interface IDadataService
    {
        Task<TResponce> GetAccountsById<TResponce>(string inn, string kpp, ECounterpartyType type);
    }
}
