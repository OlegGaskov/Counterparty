using AutoMapper;
using Counterparty.Api.ViewModels;
using Counterparty.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Counterparty.Api.LogicLayer.Mapping
{
    public class CounterpartyProfile : Profile
    {
        public CounterpartyProfile()
        {
            CreateMap<CounterpartyViewModel, CounterpartyModel>();
            CreateMap<CounterpartyModel, CounterpartyViewModel>();
        }
    }
}
