using Counterparty.Api.DataAccessLayer.Models;
using LiteDB;
using Microsoft.Extensions.Options;
using System;

namespace Counterparty.Api.DataAccessLayer.LiteDB
{
    public class CounterpartyDbContext
    {
        public LiteDatabase Context { get; }

        public CounterpartyDbContext(IOptions<CounterpartyDbConfig> options)
        {
            try
            {
                var db = new LiteDatabase(options.Value._databasePath);
                if (db != null)
                    Context = db;
            }
            catch (Exception ex)
            {
                throw new Exception("Can`t find or create LiteDb database.", ex);
            }
        }
    }
}
