using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.DomainEventHandlers
{
    public class TransactionConvertedEventHandler : INotificationHandler<TransactionCancelledDomainEvent>
    {
        public Task Handle(TransactionCancelledDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
