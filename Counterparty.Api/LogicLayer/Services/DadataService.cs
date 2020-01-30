using Counterparty.Api.LogicLayer.Models.ServiceModels;
using Counterparty.DataAccess.Models;
using Dadata.SmallApiClient.Models;
using Microsoft.Extensions.Options;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Counterparty.Api.LogicLayer.Services
{
    public class DadataServiceConfig
    {
        public string BaseUrl { get; set; }
        public string FindByIdQuery { get; set; }
    }
    public class DadataService : IDadataService
    {
        private readonly IDadataClient _dadataClient;
        private readonly DadataServiceConfig _configSettings;
        public DadataService(IDadataClient dadataClient, IOptions<DadataServiceConfig> config)
        {
            _dadataClient = dadataClient;
            _configSettings = config.Value;
        }
        public async Task<T> GetAccountsById<T>(string inn, string kpp, ECounterpartyType type)
        {
            dynamic queryOptions = new ExpandoObject();
            queryOptions.query = inn;

            if (type == ECounterpartyType.Legal)
                queryOptions.kpp = kpp;

            return await _dadataClient.Execute<T>(HttpMethod.Post, $"{_configSettings.BaseUrl}{_configSettings.FindByIdQuery}", queryOptions);
        }
    }
}
