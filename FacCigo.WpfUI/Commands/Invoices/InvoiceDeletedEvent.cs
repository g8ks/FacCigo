using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacCigo.Commands.Invoices
{
    public class InvoiceDeletedEvent:PubSubEvent<Guid>
    {
    }
}
