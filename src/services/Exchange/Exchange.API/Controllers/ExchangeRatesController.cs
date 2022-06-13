using Microsoft.AspNetCore.Mvc;

namespace Exchange.API.Controllers
{
    public class ExchangeRatesController : ControllerBase
    {
        private IExchangeRateRepository _exchangeRateRepository;
        public ExchangeRatesController(IMediator mediator, IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }
        [Route("get/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(ExchangeRate), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAllExchange(string id)
        {
            var rates = await _exchangeRateRepository.GetExchangeRateAsync(id);
            return Ok(rates);
        }
    }
}
