using Dadata.SmallApiClient.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dadata.SmallApiClient
{
    public static class DadataServiceExtention
    {
        public static void AddDadataClient(this IServiceCollection services, string token, string secret)
        {
            services.AddSingleton<IDadataClient, DadataClient>();
            services.Configure<DadataServiceConfig>(options => {
                options.Token = token;
                options.Secret = secret;
            });
        }
    }
}
