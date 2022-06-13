

namespace Exchange.API.Controllers
{
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountQuery _accountQuery;
        private readonly IMediator _mediator;
        public AccountsController(IMediator mediator, IAccountQuery accountQuery, IAccountRepository accountRepository)
        {
            _mediator = mediator;
            _accountRepository = accountRepository;
            _accountQuery = accountQuery;
        }
        [Route("getBalanceByUser/{user}")]
        [HttpGet]
        [ProducesResponseType(typeof(AccountViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<AccountViewModel>>> GetBalanceByUser(string user)
        {
            var balance = await _accountQuery.GetBalanceByUser(user);
            return Ok(balance);
        }
        [Route("getBalanceById/{id}")]
        [HttpGet]
        [ProducesResponseType(typeof(AccountViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<AccountViewModel>> GetBalanceById(int id)
        {
            var balance = await _accountQuery.GetBalance(id);
            return Ok(balance);
        }
        [Route("create")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAccount(CreateAccountCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result)
                return BadRequest();
            return Ok();
        }
        [Route("getAllAccount")]
        [HttpGet]
        [ProducesResponseType(typeof(AccountViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<AccountViewModel>>> GetBalanceById()
        {
            var balance = await _accountQuery.GetAllAccount();
            return Ok(balance);
        }
    }
}
