using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace FacCigo
{
     public class ExamNotExisting:BusinessException
    {
        public ExamNotExisting(Guid id):base("F:008",$"The Exam {id} does not exist !")
        {
        }
    }
}
