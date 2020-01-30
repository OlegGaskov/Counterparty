using Counterparty.Api.DataAccessLayer.LiteDB;

namespace Counterparty.Api.DataAccessLayer
{
    public class BaseReposytory
    {
        protected readonly CounterpartyDbContext _context;

        public BaseReposytory(CounterpartyDbContext context)
        {
            _context = context;
        }
    }
}
