using CUDJobApiIdentity.Models;
using CUDJobAPiIdentity.Models;
using CUDJobAPiIdentity.VIewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CUDJobAPiIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<StudentSeekers> Students { get; set; }
        public DbSet<StudentEducation> Studenteducation { get; set; }
        public DbSet<StudentExperience> StudentExperience { get; set; }
        public DbSet<VolunteerExperience> VolunteerExperience { get; set; }
        public DbSet<Studentportfolio> Studentportfolio { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Jobs> JobModel { get; set; }        
        public DbSet<Address> Address { get; set; }        
        public DbSet<CountryCode> CountryCode { get; set; }        
        public DbSet<CompanyCategory> CompanyCategory { get; set; }
        public DbSet<CompanySectors> CompanySectors { get; set; }
        public DbSet<CompanyAddressCollection> CompanyAddressCollection { get; set; }
        public DbSet<StudentAddressCollection> StudentAddressCollection { get; set; }
        public DbSet<AppliedJobs> AppliedJobs { get; set; }
        public DbSet<Awards> Awards { get; set; }
        public DbSet<Memberships> Memberships { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<programs> programs { get; set; }
        public DbSet<Languages> Languages { get; set; }
        public DbSet<JobStatuses> JobStatuses { get; set; }
        public DbSet<JobCategory> JobCategory { get; set; }
        public DbSet<JobExperiences> JobExperiences { get; set; }
        public DbSet<DaysPerWeekOptions> DaysPerWeekOptions { get; set; }
        public DbSet<HoursPerWeekOptions> HoursPerWeekOptions { get; set; }
        public DbSet<StudentWorkAvailability> StudentWorkAvailability { get; set; }
        public DbSet<StudentHardSkills> StudentHardSkills { get; set; }
        public DbSet<StudentSoftSkills> StudentSoftSkills { get; set; }
        public DbSet<StudentComputerSkills> StudentComputerSkills { get; set; }
        public DbSet<Statuses> Statuses { get; set; }
        public DbSet<StatusesNotes> StatusesNotes { get; set; }
        public DbSet<Jobtypes> Jobtypes { get; set; }
        public DbSet<Job_durations> Job_durations { get; set; }
        public DbSet<Hardskills> Hardskills { get; set; }
        public DbSet<Softskills> Softskills { get; set; }
        public DbSet<Computerskills> Computerskills { get; set; }
        public DbSet<LanguageNames> LanguageNames { get; set; }
        public DbSet<LanguageLevels> LanguageLevels { get; set; }
        public DbSet<Tbl_Userloginlogs> Tbl_Userloginlogs { get; set; }
        public DbSet<Reminders> Reminders { get; set; }
        public DbSet<ReminderConfig> ReminderConfig { get; set; }
        public DbSet<ReminderLog> ReminderLog { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            builder.Entity<Address>().HasOne(m => m.CountryCode)
                                 .WithMany(m => m.address).HasForeignKey(m => m.CountryID);
            builder.Entity<Address>().HasOne(m => m.NationalityCode)
                                        .WithMany(m=>m.Nationalityaddress).HasForeignKey(m => m.Nationality);
            base.OnModelCreating(builder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
