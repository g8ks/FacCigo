using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacCigo.Commands.ETL
{
    public class ETLFileLoadedEvent:PubSubEvent<IList<object>>
    {
    }
}
