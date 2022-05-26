using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.Commands
{
    public class CreateTransactionCommand : IRequest<bool>
    {
        public decimal Amount { get; private set; }
        public int BuyerId { get; private set; }
        public string FromCurrency { get; private set; }
        public string ToCurrency { get; private set; }
        public CreateTransactionCommand()
        {

        }
        public CreateTransactionCommand(decimal amount, int buyerId, string fromCurrency,string toCurrency)
        {
            Amount = amount;
            BuyerId = buyerId;
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;


        }
    }
}
