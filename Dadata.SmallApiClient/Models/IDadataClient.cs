using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Dadata.SmallApiClient.Models
{
    public interface IDadataClient
    {
        Task<TResponce> Execute<TResponce>(HttpMethod method, string url, object query = null);
    }
}
