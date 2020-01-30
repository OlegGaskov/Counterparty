using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Counterparty.Api.DataAccessLayer.LiteDB;
using Counterparty.Api.DataAccessLayer.Models;
using Counterparty.Api.LogicLayer.Models.ServiceModels;
using Counterparty.Api.LogicLayer.Services;
using Counterparty.BusinessLogic.Models.ServiceModels;
using Counterparty.BusinessLogic.Services;
using Counterparty.DataAccess;
using Dadata.SmallApiClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Counterparty.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            //AutoMapper
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();

            //LiteDB
            services.AddScoped<CounterpartyDbContext, CounterpartyDbContext>();
            services.Configure<CounterpartyDbConfig>(options =>
            {
                options._databasePath = Configuration["DatabasePath"];
            });

            //Repositories & Services
            services.AddScoped<ICounterpartyReposytory, CounterpartyReposytory>();
            services.AddScoped<ICounterpartyService, CounterpartyService>();

            //Dadata HttpClient
            services.AddDadataClient(Configuration["Dadata:Token"], Configuration["Dadata:Secret"]);
            services.AddSingleton<IDadataService, DadataService>();
            services.Configure<DadataServiceConfig>(options =>
            {
                options.BaseUrl = Configuration["Dadata:BaseUrl"];
                options.FindByIdQuery = Configuration["Dadata:Queries:FindById"];
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { });
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Counterparty Api v1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
