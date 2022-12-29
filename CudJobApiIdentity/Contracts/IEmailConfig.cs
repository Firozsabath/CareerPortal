using CUDJobApiIdentity.DTOs;
using CUDJobAPiIdentity.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Contracts
{
    public interface IEmailConfig
    {
        string SendEmail(EmailDTO email);
        string SendEmail_jobapprove(Jobs jobmodel);
        string SendEmail_Companyapprove(Companies company);
        string SendEmail_JobApplication(StudentSeekers student, Jobs jobmodel);
    }
}
