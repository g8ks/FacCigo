﻿using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacCigo.Commands.Patients
{
    public class PatientUpdatedEvent:PubSubEvent<PatientDto>
    {
    }
}
