using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Counterparty.Api.LogicLayer.Models.ServiceModels;
using Counterparty.Api.ViewModels;
using Counterparty.BusinessLogic.Models.ServiceModels;
using Counterparty.DataAccess.Models;
using Dadata.SmallApiClient.Models.ResponceModels;
using Microsoft.AspNetCore.Mvc;

namespace Counterparty.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterpartyController : ControllerBase
    {
        private readonly ICounterpartyService _counterpartyService;
        private readonly IMapper _mapper;
        public CounterpartyController(ICounterpartyService counterpartyService, IMapper mapper)
        {
            _counterpartyService = counterpartyService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<CounterpartyViewModel> GetAll()
        {
            var counterparties = _counterpartyService.GetAll();
            return _mapper.Map<IEnumerable<CounterpartyViewModel>>(counterparties);
        }

        [HttpPost]
        public async Task<ActionResult<CounterpartyViewModel>> Post([FromBody]CounterpartyViewModel counterpartyVM, [FromServices] IDadataService dadataService)
        {
            //validation rules set in AccountViewModel
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //get account from Dadata.ru by Inn & Kpp and set account 'Fullname'
            var daDataResponce = await dadataService.GetAccountsById<FindByIdResponce>(counterpartyVM.Inn, counterpartyVM.Kpp, counterpartyVM.Type);
            
            if (!daDataResponce.suggestions.Any())
                return BadRequest("Account not found in Dadata.ru!");
            else if (daDataResponce.suggestions.Count() > 1)
                return BadRequest("Finded more than one accounts with this params in Dadata.ru!");
            else
                counterpartyVM.Fullname = daDataResponce.suggestions.First().data.name.full_with_opf;

            var counterpartyId = _counterpartyService.Add(_mapper.Map<CounterpartyModel>(counterpartyVM));
            return CreatedAtAction(nameof(GetById), new { id = counterpartyId }, counterpartyVM);
        }

        [HttpGet("{id}")]
        public ActionResult<CounterpartyViewModel> GetById(int id)
        {
            var account = _counterpartyService.Find(id);
            if (account == null)
            {
                return NotFound();
            }
            var accountVM = _mapper.Map<CounterpartyViewModel>(account);
            return accountVM;
        }
    }
}
