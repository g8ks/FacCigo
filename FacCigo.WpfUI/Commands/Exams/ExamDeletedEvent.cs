using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacCigo.Commands.Exams
{
     public class ExamDeletedEvent: PubSubEvent<Guid>
    {
    }
}
