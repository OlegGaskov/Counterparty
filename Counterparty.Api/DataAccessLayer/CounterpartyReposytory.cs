using System;
using System.Collections.Generic;
using System.Linq;
using Counterparty.Api.DataAccessLayer;
using Counterparty.Api.DataAccessLayer.LiteDB;
using Counterparty.DataAccess.Models;

namespace Counterparty.DataAccess
{
    public class CounterpartyReposytory : BaseReposytory, ICounterpartyReposytory
    {
        public CounterpartyReposytory(CounterpartyDbContext context) : base(context)
        {

        }
        public int Add(CounterpartyModel counterparty)
        {
            return _context.Context.GetCollection<CounterpartyModel>().Insert(counterparty);
        }

        public CounterpartyModel Find(int id)
        {
            return _context.Context.GetCollection<CounterpartyModel>().Find(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<CounterpartyModel> FindAll()
        {
            return _context.Context.GetCollection<CounterpartyModel>().FindAll();
        }
    }
}
