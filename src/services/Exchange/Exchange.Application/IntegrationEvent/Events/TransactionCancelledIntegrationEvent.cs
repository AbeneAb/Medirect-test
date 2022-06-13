using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.IntegrationEvent.Events
{
    public record TransactionCancelledIntegrationEvent : Event 
    {
        public Transaction Transaction { get; set; }
        public TransactionCancelledIntegrationEvent( Transaction transaction)
        {
            Transaction = transaction;
        }
    }
}
