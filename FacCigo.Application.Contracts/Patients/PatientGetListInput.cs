using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
    public class PatientGetListInput: PagedAndSortedResultRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set;}
        public string PhoneNumber { get; set; }
        public string Search { get; set; }
    }
}
