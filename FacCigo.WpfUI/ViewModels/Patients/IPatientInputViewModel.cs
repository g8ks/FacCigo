using System;
using System.Collections.Generic;
using System.Text;

namespace FacCigo.ViewModels.Patients
{
    public interface IPatientInputViewModel
    {
        void UpdateModel(PatientDto dto);
    }
}
