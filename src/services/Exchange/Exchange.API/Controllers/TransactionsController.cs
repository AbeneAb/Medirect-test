using Microsoft.AspNetCore.Mvc;

namespace Exchange.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionQuery _transactionQuery;
        public TransactionsController(IMediator mediator,ILogger<TransactionsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTransaction(CreateTransactionCommand command, [FromHeader(Name = "x-requestid")] string requestId) 
        {
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest();
            return Ok();
        }
        [Route("{transactionId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(TransactionSummaryViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GettransactionIdAsync(int transactionId)
        {
            try
            {
                var order = await _transactionQuery.GetTransactionsAsync(transactionId);

                return Ok(order);
            }
            catch
            {
                return NotFound();
            }
        }
        [Route("{buyerId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(TransactionSummaryViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetTransactionByUserId(int buyerId)
        {
            try
            {
                var order = await _transactionQuery.GetOrdersFromUserAsync(buyerId);

                return Ok(order);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
