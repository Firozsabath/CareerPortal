
using CUDJobApiIdentity.Models;
using CUDJobApiIdentity.VIewModels;
using CUDJobAPiIdentity.Models;
using CUDJobAPiIdentity.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CUDJobApiIdentity.Contracts
{
    public interface ISupportFunction
    {
        public StudentProfile ConvertToStudentProfile(StudentSeekers entity);
        public IList<StudentProfile> ConvertToStudentProfileList(List<StudentSeekers> entity);
        public CompanyViewModel ConvertToCompanyViewModel(Companies entity);
        public IList<CompanyViewModel> ConvertToCompanyViewModelList(List<Companies> entity);
        public IList<JobDetails> ConvertJobDetailsList(IList<Jobs> entity);
        public JobDetails ConvertJobDetais(Jobs entity);
        public IList<JobApplyDetails> ConvertToApplyJobViewModelList(List<AppliedJobs> entity);
        public IList<JobApplyDetailsBystudent> ConvertToApplyJobViewByStudent(List<AppliedJobs> entity);
        public StudentPersonalview ConvertStudentPersonal(StudentSeekers entity);
    }
}
